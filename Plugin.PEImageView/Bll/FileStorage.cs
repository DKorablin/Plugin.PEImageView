﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AlphaOmega.Debug;
using Plugin.PEImageView.Directory;
using SAL.Windows;

namespace Plugin.PEImageView.Bll
{
	internal class FileStorage : IDisposable
	{
		private readonly Object _binLock = new Object();
		private readonly Dictionary<String, PEFile> _binaries = new Dictionary<String, PEFile>();
		private readonly Dictionary<String, FileSystemWatcher> _binaryWatcher = new Dictionary<String, FileSystemWatcher>();
		private readonly PluginWindows _plugin;

		public event EventHandler<PeListChangedEventArgs> PeListChanged;

		internal FileStorage(PluginWindows plugin)
		{
			this._plugin = plugin ?? throw new ArgumentNullException(nameof(plugin));
			this._plugin.Settings.PropertyChanged += Settings_PropertyChanged;
		}

		/// <summary>Получить информацию по PE файлу. Если файл не открыт, то открыть его</summary>
		/// <param name="filePath">Путь к файлу, информацию по которому необходимо почитать</param>
		/// <returns>Информация по PE/COFF файлу или null</returns>
		public PEFile LoadFile(String filePath)
			=> this.LoadFile(filePath, false);

		/// <summary>Получить информацию по PE файлу</summary>
		/// <param name="filePath">Путь к файлу, информацию по которому необходимо почитать</param>
		/// <param name="noLoad">Поискать файл в уже подгруженных файлах и если такой файл не найден не загружать</param>
		/// <returns>Информация по PE/COFF файлу или null</returns>
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
				return null;//Это необходимо для отсечки файлов, которые были загружены через память

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
							loader = new StreamLoader(stream);//.FromFile(filePath);
							break;
						case PluginSettings.PeLoader.Win32Loader:
							loader = Win32Loader.FromFile(filePath);
							break;
						default:
							throw new NotSupportedException(this._plugin.Settings.Loader.ToString());
						}
						result = new PEFile(filePath, loader);
						this._binaries.Add(filePath, result);
						if(!this._binaryWatcher.ContainsKey(filePath)//При обновлении файла удаляется только файл, а не его монитор
							&& this._plugin.Settings.MonitorFileChange)
							this.RegisterFileWatcher(filePath);
					}
				}
			return result;
		}

		/// <summary>Закрыть ранее открытый файл</summary>
		/// <param name="filePath">Путь к файлу для закрывания</param>
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
				for(Int32 loop = windows.Length - 1;loop >= 0;loop--)
				{
					IWindow wnd = windows[loop];
					DocumentBase ctrl = wnd.Control as DocumentBase;
					if(ctrl != null && ctrl.FilePath == filePath)
						wnd.Close();
				}
				if(filePath.StartsWith(Constant.BinaryFile))//Бинарный файл удаляется сразу из списка после закрытия
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

		/// <summary>Добавить файл из памяти в список открытых файлов</summary>
		/// <param name="memFile">Файл из памяти</param>
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

		/// <summary>Добавить файл в список открытых файлов</summary>
		/// <param name="filePath">Путь к файлу</param>
		public Boolean OpenFile(String filePath)
		{
			if(String.IsNullOrEmpty(filePath))
				throw new ArgumentNullException(nameof(filePath));
			if(filePath.StartsWith(Constant.BinaryFile))
				return false;//Это необходимо для отсечки файлов, которые были загружены через память

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

		public void CloseFile(String filePath)
		{
			if(String.IsNullOrEmpty(filePath))
				throw new ArgumentNullException(nameof(filePath));

			String[] loadedFiles = this._plugin.Settings.LoadedFiles;
			List<String> files = new List<String>(loadedFiles);
			if(files.Remove(filePath))
			{//Если это файл из памяти, то его нет в списке файлов
				this._plugin.Settings.LoadedFiles = files.ToArray();
				this._plugin.HostWindows.Plugins.Settings(this._plugin).SaveAssemblyParameters();
				this.OnPeListChanged(PeListChangeType.Removed, filePath);
			}
		}

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
		/// <summary>Изменился список загруженных файлов</summary>
		/// <param name="type">Тип изменения</param>
		/// <param name="filePath">Путь к файлу, на которм произошло изменение</param>
		private void OnPeListChanged(PeListChangeType type, String filePath)
			=> this.PeListChanged?.Invoke(this, new PeListChangedEventArgs(type, filePath));

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
						if(info.Exists == false)
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

		/// <summary>Получить уникальное наименование бинарного файла</summary>
		/// <param name="index">Индекс, если файл с таким наименованием уже загружен</param>
		/// <returns>Уникальное наименование файла</returns>
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