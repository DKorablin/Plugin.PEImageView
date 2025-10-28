using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using AlphaOmega.Debug;
using Plugin.PEImageView.Bll;
using Plugin.PEImageView.Directory;
using Plugin.PEImageView.Properties;
using SAL.Flatbed;
using SAL.Windows;

namespace Plugin.PEImageView
{
	/// <summary>
	/// Interface:
	/// When opening a file, a panel opens on the left with the PE File's TOC in the following format:
	/// ListView(TreeView) - For main properties [TOCs]
	/// ListView - For properties of main properties [TOCs]
	/// TextArea - For the description of the main property [TOC]
	/// A pointer to the PEInfo of the opened file is added to PluginWindows.
	///
	/// Double-clicking a TOC element opens the document corresponding to the specified TOC, which will then display information for each TOC separately.
	/// The path to the PE file whose TOC is being displayed is added to Window.Settings[].
	///
	/// When opening a document, the document works with the PEInfo object in PluginWindows. /// When the program opens and one of the TOC documents is opened, the document requests a PEInfo object from PluginWindows, and if necessary, PluginWindows creates the appropriate PEInfo and assigns it to the document.
	/// </summary>
	public class PluginWindows : IPlugin, IPluginSettings<PluginSettings>
	{
		private TraceSource _trace;
		private PluginSettings _settings;
		private readonly Object _binLock = new Object();
		private FileStorage _binaries;
		private Dictionary<PeHeaderType, Type> _directoryViewers;
		private Dictionary<String, DockState> _documentTypes;

		internal TraceSource Trace => this._trace ?? (this._trace = PluginWindows.CreateTraceSource<PluginWindows>());

		internal IHostWindows HostWindows { get; }

		private IMenuItem MenuPeInfo { get; set; }
		private IMenuItem MenuWinApi { get; set; }

		/// <summary>Settings for interaction from the host</summary>
		Object IPluginSettings.Settings => this.Settings;

		/// <summary>Settings for interaction from the plugin</summary>
		public PluginSettings Settings
		{
			get
			{
				if(this._settings == null)
				{
					this._settings = new PluginSettings();
					this.HostWindows.Plugins.Settings(this).LoadAssemblyParameters(this._settings);
				}
				return this._settings;
			}
		}

		/// <summary>Open File Storage</summary>
		internal FileStorage Binaries
		{
			get
			{
				if(this._binaries == null)
					lock(this._binLock)
						if(this._binaries == null)
							this._binaries = new FileStorage(this);
				return this._binaries;
			}
		}

		internal Dictionary<PeHeaderType, Type> DirectoryViewers
		{
			get
			{
				if(this._directoryViewers == null)
					this._directoryViewers = new Dictionary<PeHeaderType, Type>
					{
						{ PeHeaderType.DIRECTORY_IMPORT, typeof(DocumentImport) },
						{ PeHeaderType.DIRECTORY_EXPORT, typeof(DocumentExport) },
						{ PeHeaderType.DIRECTORY_DEBUG, typeof(DocumentDebug) },
						{ PeHeaderType.DIRECTORY_COR_METADATA, typeof(DocumentMetadata) },
						{ PeHeaderType.DIRECTORY_COR_RESOURCE, typeof(DocumentCorResources) },
						{ PeHeaderType.DIRECTORY_BOUND_IMPORT, typeof(DocumentBoundImport) },
						{ PeHeaderType.DIRECTORY_DELAY_IMPORT, typeof(DocumentDelayImport) },
						{ PeHeaderType.DIRECTORY_RELOCATION, typeof(DocumentRelocation) },
						{ PeHeaderType.DIRECTORY_RESOURCE, typeof(DocumentResources) },
					};
				return this._directoryViewers;
			}
		}

		private Dictionary<String, DockState> DocumentTypes
		{
			get
			{
				if(this._documentTypes == null)
					this._documentTypes = new Dictionary<String, DockState>()
					{
						{ typeof(DocumentDebug).ToString(), DockState.Document },
						{ typeof(DocumentExport).ToString(), DockState.Document },
						{ typeof(DocumentImport).ToString(), DockState.Document },
						{ typeof(DocumentMetadata).ToString(), DockState.Document },
						{ typeof(DocumentCorResources).ToString(), DockState.Document },
						{ typeof(DocumentBoundImport).ToString(), DockState.Document },
						{ typeof(DocumentDelayImport).ToString(), DockState.Document },
						{ typeof(DocumentRelocation).ToString(), DockState.Document },
						{ typeof(DocumentResources).ToString(), DockState.Document },
						{ typeof(DocumentBinary).ToString(), DockState.Document },
						{ typeof(PanelTOC).ToString(), DockState.DockRightAutoHide },
					};
				return this._documentTypes;
			}
		}

		public PluginWindows(IHostWindows hostWindows)
			=> this.HostWindows = hostWindows ?? throw new ArgumentNullException(nameof(hostWindows));

		public IWindow GetPluginControl(String typeName, Object args)
			=> this.CreateWindow(typeName, false, args);

		/// <summary>Get the type of the object that is used to search for the object</summary>
		/// <returns>Reflection on the type of object used for searching</returns>
		public Type GetEntityType()
			=> typeof(PEFile);

		/// <summary>Create an instance of an object to search through reflection</summary>
		/// <remarks>To get a list, use <see cref="GetSearchObjects"/></remarks>
		/// <param name="filePath">The path to the element by which to create an instance for the search</param>
		/// <returns>The given object instance</returns>
		public Object CreateEntityInstance(String filePath)
		{
			PEFile result = new PEFile(filePath, StreamLoader.FromFile(filePath));
			return result;
		}

		/// <summary>Return objects for search, at the choice of the client, which will be searched</summary>
		/// <param name="folderPath">Path to folder where search for files</param>
		/// <returns>An array of files to search for, or null if the client didn't select anything</returns>
		public String[] GetSearchObjects(String folderPath)
		{
			List<String> result = new List<String>();
			foreach(String filePath in System.IO.Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories))
			{
				String ext = Path.GetExtension(filePath).ToLowerInvariant();
				switch(ext)
				{//TODO: After transferring it to .NET 4 change to Directory.EnumerateFiles
				case ".dll":
				case ".exe":
					result.Add(filePath);
					break;
				}
			}

			return result.ToArray();
		}

		Boolean IPlugin.OnConnection(ConnectMode mode)
		{
			IHostWindows host = this.HostWindows;
			if(host == null)
				this.Trace.TraceEvent(TraceEventType.Error, 10, "Plugin {0} requires {1}", this, typeof(IHostWindows));
			else
			{
				IMenuItem menuView = host.MainMenu.FindMenuItem("View");
				if(menuView == null)
					this.Trace.TraceEvent(TraceEventType.Error, 10, "Menu item 'View' not found");
				else
				{
					this.MenuWinApi = menuView.FindMenuItem("Executables");
					if(this.MenuWinApi == null)
					{
						this.MenuWinApi = menuView.Create("Executables");
						this.MenuWinApi.Name = "View.Executable";
						menuView.Items.Add(this.MenuWinApi);
					}

					this.MenuPeInfo = this.MenuWinApi.Create("&PE/CLI View");
					this.MenuPeInfo.Name = "View.Executable.PEView";
					this.MenuPeInfo.Click += (sender, e) => this.CreateWindow(typeof(PanelTOC).ToString(), true, null);

					this.MenuWinApi.Items.Add(this.MenuPeInfo);
					return true;
				}
			}
			return false;
		}

		Boolean IPlugin.OnDisconnection(DisconnectMode mode)
		{
			if(this.MenuPeInfo != null)
				this.HostWindows.MainMenu.Items.Remove(this.MenuPeInfo);
			if(this.MenuWinApi != null && this.MenuWinApi.Items.Count == 0)
				this.HostWindows.MainMenu.Items.Remove(this.MenuWinApi);

			NodeExtender.DisposeFonts();

			this._binaries?.Dispose();
			return true;
		}

		internal String FormatValue(Object value)
			=> value == null
				? null
				: this.FormatValue(value.GetType(), value);

		internal String FormatValue(MemberInfo info, Object value)
		{
			if(value == null)
				return null;

			Type type = info.GetMemberType();

			if(type.IsEnum)
				return value.ToString();
			else if(type == typeof(Char))
			{
				switch((Char)value)
				{
				case '\'': return "\\\'";
				case '\"': return "\\\"";
				case '\0': return "\\0";
				case '\a': return "\\a";
				case '\b': return "\\b";
				case '\f': return "\\b";
				case '\t': return "\\t";
				case '\n': return "\\n";
				case '\r': return "\\r";
				case '\v': return "\\v";
				default: return value.ToString();
				}
			} else if(value is IFormattable fValue)
			{
				type = type.GetRealType();//INullable<Enum>
				if(type.IsEnum)
					return value.ToString();

				switch(Convert.GetTypeCode(value))
				{
				case TypeCode.Byte:
				case TypeCode.SByte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					if(this.Settings.ShowAsHexValue)
						return "0x" + fValue.ToString("X", CultureInfo.CurrentCulture);
					else
						return fValue.ToString("n0", CultureInfo.CurrentCulture);
				default:
					return value.ToString();
				}
			} else
			{
				Type elementType = type.HasElementType ? type.GetElementType() : null;
				if(elementType != null && elementType.IsPrimitive && type.BaseType == typeof(Array))
				{
					Int32 index = 0;
					Array arr = (Array)value;
					StringBuilder values = new StringBuilder($"{elementType}[{this.FormatValue(arr.Length)}]");
					if(this.Settings.MaxArrayDisplay > 0)
					{
						values.Append(" { ");
						foreach(Object item in arr)
						{
							if(index++ > this.Settings.MaxArrayDisplay)
							{
								values.Append("...");
								break;
							}
							values.Append((this.FormatValue(item) ?? Resources.NullString) + ", ");
						}
						values.Append('}');
					}
					return values.ToString();
				} else if(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
					return type.ToString();//Added for properties which return type is IEnumerable
				else
					return value.ToString();
			}
		}

		internal Object GetSectionData(PeHeaderType type, String nodeName, String filePath)
		{
			PEFile pe = this.Binaries.LoadFile(filePath);
			return GetSectionData(type, nodeName, pe);
		}

		/// <summary>Get an object corresponding to a specific enum identifier</summary>
		/// <param name="type">Header type</param>
		/// <param name="filePath">Path to the PE file</param>
		/// <returns></returns>
		internal static Object GetSectionData(PeHeaderType type, String nodeName, PEFile info)
		{
			switch(type)
			{
			case PeHeaderType.IMAGE_DOS_HEADER://DOS Header
				return info.Header.HeaderDos;
			case PeHeaderType.IMAGE_SECTION_HEADER:
				return info.Header.Sections;
			case PeHeaderType.IMAGE_SECTION:
				return info.Sections.GetSection(nodeName).Header;
			case PeHeaderType.IMAGE_NT_HEADERS://NT Header
				if(info.Header.Is64Bit)
					return info.Header.HeaderNT64;
				else
					return info.Header.HeaderNT32;
			case PeHeaderType.IMAGE_FILE_HEADER://File Header
				if(info.Header.Is64Bit)
					return info.Header.HeaderNT64.FileHeader;
				else
					return info.Header.HeaderNT32.FileHeader;
			case PeHeaderType.IMAGE_COFF_HEADER://COFF header
				return info.Header.SymbolTable;
			case PeHeaderType.IMAGE_OPTIONAL_HEADER://Optional header
				if(info.Header.Is64Bit)
					return info.Header.HeaderNT64.OptionalHeader;
				else
					return info.Header.HeaderNT32.OptionalHeader;
			case PeHeaderType.DIRECTORY_ARCHITECTURE:
				return info.Architecture;
			case PeHeaderType.DIRECTORY_IAT:
				return info.Iat;
			case PeHeaderType.DIRECTORY_BOUND_IMPORT://Bound Import
				return info.BoundImport;
			case PeHeaderType.DIRECTORY_COM_DECRIPTOR:
				if(info.ComDescriptor == null)
					return info[WinNT.IMAGE_DIRECTORY_ENTRY.CLR_HEADER];
				else
					return info.ComDescriptor.Cor20Header;
			case PeHeaderType.DIRECTORY_COR_CMT:
				return info.ComDescriptor.CodeManagerTable;
			case PeHeaderType.DIRECTORY_COR_EAT:
				return info.ComDescriptor.Eat;
			case PeHeaderType.DIRECTORY_COR_METADATA:
				return info.ComDescriptor.MetaData;
			case PeHeaderType.DIRECTORY_COR_MNH:
				return info.ComDescriptor.ManagedNativeHeader;
			case PeHeaderType.DIRECTORY_COR_SN:
				return info.ComDescriptor.StrongNameSignature;
			case PeHeaderType.DIRECTORY_COR_VTABLE:
				return info.ComDescriptor.VTable;
			case PeHeaderType.DIRECTORY_COR_RESOURCE:
				if(info.ComDescriptor.Resources == null)
					return info.ComDescriptor.Cor20Header[WinNT.COR20_DIRECTORY_ENTRY.Resources];
				else
					return info.ComDescriptor.Resources;
			case PeHeaderType.DIRECTORY_DEBUG:
				return info.Debug;
			case PeHeaderType.DIRECTORY_DELAY_IMPORT:
				return info.DelayImport;
			case PeHeaderType.DIRECTORY_EXPORT:
				return info.Export;
			case PeHeaderType.DIRECTORY_IMPORT:
				return info.Import;
			case PeHeaderType.DIRECTORY_LOAD_CONFIG:
				return info.LoadConfig;
			case PeHeaderType.DIRECTORY_RESOURCE:
				return info.Resource;
			case PeHeaderType.DIRECTORY_SECURITY:
				return info.Certificate;
			case PeHeaderType.DIRECTORY_RELOCATION:
				return info.Relocations;
			case PeHeaderType.DIRECTORY_GLOBALPTR:
				return info.GlobalPtr;
			case PeHeaderType.DIRECTORY_EXCEPTION:
				return info.ExceptionTable;
			case PeHeaderType.DIRECTORY_TLS:
				return info.Tls;
			default:
				throw new NotImplementedException($"Data retrieval for type '{type}' not found");
			}
		}

		internal IWindow CreateWindow(String typeName, Boolean searchForOpened, Object args)
			=> this.DocumentTypes.TryGetValue(typeName, out DockState state)
				? this.HostWindows.Windows.CreateWindow(this, typeName, searchForOpened, state, args)
				: null;

		internal IWindow CreateWindow(PeHeaderType typeName, DocumentBaseSettings args)
			=> this.DirectoryViewers.TryGetValue(typeName, out Type type)
				? this.HostWindows.Windows.CreateWindow(this, type.ToString(), true, DockState.Document, args)
				: null;

		internal IWindow CreateWindow<T, A>(A args) where T : class, IPluginSettings<A> where A : class
		{
			String type = typeof(T).ToString();
			return this.DocumentTypes.TryGetValue(type, out DockState state)
				? this.HostWindows.Windows.CreateWindow(this, type, true, state, args)
				: null;
		}

		internal static TraceSource CreateTraceSource<T>(String name = null) where T : IPlugin
		{
			TraceSource result = new TraceSource(typeof(T).Assembly.GetName().Name + name);
			result.Switch.Level = SourceLevels.All;
			result.Listeners.Remove("Default");
			result.Listeners.AddRange(System.Diagnostics.Trace.Listeners);
			return result;
		}
	}
}