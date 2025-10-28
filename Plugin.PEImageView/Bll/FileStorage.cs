using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using AlphaOmega.Debug;
using Plugin.PEImageView.Directory;
using SAL.Windows;

namespace Plugin.PEImageView.Bll
{
	/// <summary>
	/// Manages the storage and lifecycle of PE (Portable Executable) files loaded into the application.
	/// Handles file loading, monitoring for changes, and cleanup of resources.
	/// </summary>
	internal class FileStorage : IDisposable
	{
		private readonly Object _binLock = new Object();
		private readonly Dictionary<String, PEFile> _binaries = new Dictionary<String, PEFile>();
		private readonly Dictionary<String, FileSystemWatcher> _binaryWatcher = new Dictionary<String, FileSystemWatcher>();
		private readonly PluginWindows _plugin;

		/// <summary>Event raised when the list of loaded PE files changes (files added, removed, or modified)</summary>
		public event EventHandler<PeListChangedEventArgs> PeListChanged;

		/// <summary>Initializes a new instance of the FileStorage class</summary>
		/// <param name="plugin">The plugin instance that owns this storage</param>
		/// <exception cref="ArgumentNullException">Thrown when plugin is null</exception>
		internal FileStorage(PluginWindows plugin)
		{
			this._plugin = plugin ?? throw new ArgumentNullException(nameof(plugin));
			this._plugin.Settings.PropertyChanged += Settings_PropertyChanged;
		}

		/// <summary>Get information about a PE file. If the file is not open, open it.</summary>
		/// <param name="filePath">Path to the file whose information you want to read.</param>
		/// <returns>Information about the PE/COFF file or null.</returns>
		public PEFile LoadFile(String filePath)
			=> this.LoadFile(filePath, false);

		/// <summary>Get information about a PE file</summary>
		/// <param name="filePath">Path to the file whose information you want to read</param>
		/// <param name="noLoad">Search for the file in already loaded files and, if such a file is not found, do not load</param>
		/// <returns>Information about the PE/COFF file or null</returns>
		public PEFile LoadFile(String filePath, Boolean noLoad)
		{
			if(String.IsNullOrEmpty(filePath))
				throw new ArgumentNullException(nameof(filePath));

			PEFile result;
			if(noLoad)
			{
				this._binaries.TryGetValue(filePath, out result);
				return result;
			}

			if(!File.Exists(filePath))
				return null;//This is necessary to cut off files that were loaded through memory.

			result = this.LoadFile(filePath, true);
			if(result == null)
				lock(this._binLock)
				{
					result = this.LoadFile(filePath, true);
					if(result == null)
					{
						IImageLoader loader;
						switch(this._plugin.Settings.Loader)
						{
						case PluginSettings.PeLoader.StreamLoader:
							FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
							loader = new StreamLoader(stream);
							break;
						case PluginSettings.PeLoader.Win32Loader:
							loader = Win32Loader.FromFile(filePath);
							break;
						default:
							throw new NotSupportedException(this._plugin.Settings.Loader.ToString());
						}
						result = new PEFile(filePath, loader);
						this._binaries.Add(filePath, result);
						if(!this._binaryWatcher.ContainsKey(filePath)//When you update a file, only the file is deleted, not its monitor.
							&& this._plugin.Settings.MonitorFileChange)
							this.RegisterFileWatcher(filePath);
					}
				}
			return result;
		}

		/// <summary>Closes all windows displaying the specified file and unloads it from memory</summary>
		/// <param name="filePath">Path to the file to unload</param>
		/// <exception cref="ArgumentNullException">Thrown when filePath is null or empty</exception>
		public void UnloadFile(String filePath)
		{
			if(String.IsNullOrEmpty(filePath))
				throw new ArgumentNullException(nameof(filePath));

			PEFile info = this.LoadFile(filePath, true);
			if(info == null)
				return;//File already unloaded

			try
			{
				IWindow[] windows = this._plugin.HostWindows.Windows.ToArray();
				for(Int32 loop = windows.Length - 1; loop >= 0; loop--)
				{
					IWindow wnd = windows[loop];
					DocumentBase ctrl = wnd.Control as DocumentBase;
					if(ctrl != null && ctrl.FilePath == filePath)
						wnd.Close();
				}
				if(filePath.StartsWith(Constant.BinaryFile))//The binary file is removed from the list immediately after closing.
					this.OnPeListChanged(PeListChangeType.Removed, filePath);
			} finally
			{
				info.Dispose();
				this._binaries.Remove(filePath);
				this.UnregisterFileWatcher(filePath);
			}
		}

		/// <summary>Register file monitoring for delete or change</summary>
		/// <param name="filePath">The path to file register for watching</param>
		/// <exception cref="ArgumentNullException">filePath is null or empty string</exception>
		/// <exception cref="FileNotFoundException">File not found</exception>
		public void RegisterFileWatcher(String filePath)
		{
			if(String.IsNullOrEmpty(filePath))
				throw new ArgumentNullException(nameof(filePath));
			if(!File.Exists(filePath))
				throw new FileNotFoundException("File not found", filePath);

			FileSystemWatcher watcher = new FileSystemWatcher(Path.GetDirectoryName(filePath), Path.GetFileName(filePath))
			{
				NotifyFilter = NotifyFilters.LastWrite,
			};
			watcher.Deleted += new FileSystemEventHandler(watcher_Changed);
			watcher.Changed += new FileSystemEventHandler(watcher_Changed);
			watcher.EnableRaisingEvents = true;
			this._binaryWatcher.Add(filePath, watcher);
		}

		/// <summary>Stops monitoring a file for changes and disposes the associated watcher</summary>
		/// <param name="filePath">Path to the file to stop monitoring</param>
		/// <exception cref="ArgumentNullException">Thrown when filePath is null or empty</exception>
		public void UnregisterFileWatcher(String filePath)
		{
			if(String.IsNullOrEmpty(filePath))
				throw new ArgumentNullException(nameof(filePath));

			if(this._binaryWatcher.TryGetValue(filePath, out FileSystemWatcher watcher))
			{
				watcher.Dispose();
				this._binaryWatcher.Remove(filePath);
			}
		}

		/// <summary>Add a file from memory to the list of open files</summary>
		/// <param name="memFile">File from memory</param>
		public void OpenFile(Byte[] memFile)
		{
			if(memFile == null || memFile.Length == 0)
				throw new ArgumentNullException(nameof(memFile));

			String name;
			lock(this._binLock)
			{
				name = this.GetBinaryUniqueName(0);
				PEFile info = new PEFile(name, new StreamLoader(new MemoryStream(memFile)));
				this._binaries.Add(name, info);
			}
			this.OnPeListChanged(PeListChangeType.Added, name);
		}

		/// <summary>Adds a file to the list of tracked files and notifies listeners of the change</summary>
		/// <param name="filePath">Path to the file to open</param>
		/// <returns>True if the file was added successfully, false if it was already in the list</returns>
		/// <exception cref="ArgumentNullException">Thrown when filePath is null or empty</exception>
		public Boolean OpenFile(String filePath)
		{
			if(String.IsNullOrEmpty(filePath))
				throw new ArgumentNullException(nameof(filePath));
			if(filePath.StartsWith(Constant.BinaryFile))
				return false;//This is necessary to cut off files that were loaded through memory.

			String[] loadedFiles = this._plugin.Settings.LoadedFiles;
			if(loadedFiles.Contains(filePath))
				return false;
			else
			{
				List<String> files = new List<String>(loadedFiles) {
					filePath,
				};

				this._plugin.Settings.LoadedFiles = files.ToArray();
				this._plugin.HostWindows.Plugins.Settings(this._plugin).SaveAssemblyParameters();
				this.OnPeListChanged(PeListChangeType.Added, filePath);
				return true;
			}
		}

		/// <summary>Removes a file from the list of tracked files and notifies listeners of the change</summary>
		/// <param name="filePath">Path to the file to close</param>
		/// <exception cref="ArgumentNullException">Thrown when filePath is null or empty</exception>
		public void CloseFile(String filePath)
		{
			if(String.IsNullOrEmpty(filePath))
				throw new ArgumentNullException(nameof(filePath));

			String[] loadedFiles = this._plugin.Settings.LoadedFiles;
			List<String> files = new List<String>(loadedFiles);
			if(files.Remove(filePath))
			{//If this is a file from memory, then it is not in the file list.
				this._plugin.Settings.LoadedFiles = files.ToArray();
				this._plugin.HostWindows.Plugins.Settings(this._plugin).SaveAssemblyParameters();
				this.OnPeListChanged(PeListChangeType.Removed, filePath);
			}
		}

		/// <summary>Releases all resources used by the FileStorage instance</summary>
		public void Dispose()
		{
			lock(this._binLock)
			{
				this._plugin.Settings.PropertyChanged -= Settings_PropertyChanged;
				foreach(String key in this._binaries.Keys.ToArray())
				{
					PEFile info = this._binaries[key];
					info.Dispose();
				}
				this._binaries.Clear();
				foreach(String key in this._binaryWatcher.Keys)
					this._binaryWatcher[key].Dispose();
				this._binaryWatcher.Clear();
			}
		}

		/// <summary>Raises the PeListChanged event with the specified change type and file path</summary>
		/// <param name="type">Type of change that occurred</param>
		/// <param name="filePath">Path to the file that changed</param>
		private void OnPeListChanged(PeListChangeType type, String filePath)
			=> this.PeListChanged?.Invoke(this, new PeListChangedEventArgs(type, filePath));

		/// <summary>Handles changes to plugin settings, particularly the MonitorFileChange setting</summary>
		/// <param name="sender">The source of the property change event</param>
		/// <param name="e">Event arguments containing the name of the changed property</param>
		private void Settings_PropertyChanged(Object sender, PropertyChangedEventArgs e)
		{
			switch(e.PropertyName)
			{
			case nameof(PluginSettings.MonitorFileChange):
				if(this._plugin.Settings.MonitorFileChange)
				{
					if(this._binaryWatcher.Count == 0)
						lock(this._binLock)
						{
							if(this._binaryWatcher.Count == 0)
								foreach(String filePath in this._binaries.Keys)
									if(File.Exists(filePath))
										this.RegisterFileWatcher(filePath);
						}
				} else
				{
					if(this._binaryWatcher.Count > 0)
						lock(this._binLock)
						{
							if(this._binaryWatcher.Count > 0)
								foreach(String key in this._binaryWatcher.Keys)
									this._binaryWatcher[key].Dispose();
							this._binaryWatcher.Clear();
						}
				}
				break;
			}
		}

		/// <summary>Handles file system change events for monitored files</summary>
		/// <param name="sender">The source of the file system event</param>
		/// <param name="e">Event arguments containing information about the file system change</param>
		private void watcher_Changed(Object sender, FileSystemEventArgs e)
		{
			FileSystemWatcher watcher = (FileSystemWatcher)sender;
			watcher.EnableRaisingEvents = false;
			try
			{
				switch(e.ChangeType)
				{
				case WatcherChangeTypes.Changed:
					FileInfo info = new FileInfo(e.FullPath);

					do
					{
						if(!info.Exists)
							goto case WatcherChangeTypes.Deleted;

						try
						{
							using(FileStream s = info.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
							{ }// File was modified and unlocked.

							lock(this._binLock)//Closing old file
							{
								this._binaries[e.FullPath].Dispose();
								this._binaries.Remove(e.FullPath);
							}

							this.OnPeListChanged(PeListChangeType.Changed, e.FullPath);
							break;
						} catch(IOException exc) when((exc.HResult & 0x0000FFFF) == 32)
						{//Sharing violation
							System.Threading.Thread.Sleep(1000);
						}
						info.Refresh();
					} while(true);
					break;
				case WatcherChangeTypes.Deleted:
				case WatcherChangeTypes.Renamed:
					lock(this._binLock)
					{
						this._binaries[e.FullPath].Dispose();
						this._binaries.Remove(e.FullPath);
					}
					this.OnPeListChanged(PeListChangeType.Removed, e.FullPath);
					break;
				}
			} finally
			{
				watcher.EnableRaisingEvents = true;
			}
		}

		/// <summary>Get the unique name of a binary file</summary>
		/// <param name="index">Index, if a file with that name is already loaded</param>
		/// <returns>Unique file name</returns>
		private String GetBinaryUniqueName(UInt32 index)
		{
			String indexName = index > 0
				? $"{Constant.BinaryFile}[{index}]"
				: Constant.BinaryFile;

			return this._binaries.ContainsKey(indexName)
				? GetBinaryUniqueName(checked(index + 1))
				: indexName;
		}
	}
}