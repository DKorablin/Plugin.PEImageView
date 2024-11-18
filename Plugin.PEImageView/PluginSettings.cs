using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Plugin.PEImageView
{
	public class PluginSettings : INotifyPropertyChanged
	{
		private Boolean _monitorFileChange = false;
		private Boolean _showAsHexValue = false;
		private UInt32 _maxArrayDisplay = 10;
		private Boolean _showBaseMetaTables = false;
		private PeLoader _loader = PeLoader.StreamLoader;
		private String _loadedFilesI = null;

		public enum PeLoader
		{
			Win32Loader,
			StreamLoader,
		}

		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Show integer value in hexadecimal format")]
		public Boolean ShowAsHexValue
		{
			get => this._showAsHexValue;
			set => this.SetField(ref this._showAsHexValue, value, nameof(ShowAsHexValue));
		}

		[Category("Appearance")]
		[DefaultValue(typeof(UInt32), "10")]
		[Description("Maximum items in array to display")]
		public UInt32 MaxArrayDisplay
		{
			get => this._maxArrayDisplay;
			set => this.SetField(ref this._maxArrayDisplay, value, nameof(MaxArrayDisplay));
		}

		[Category("Data")]
		[DefaultValue(PeLoader.StreamLoader)]
		[Description("Loader used for loading PE files.\nWin32Loader - Use Win32 API LoadLibrary\nStreamLoader - Use System.IO.FileStream")]
		public PeLoader Loader
		{
			get => this._loader;
			set => this.SetField(ref this._loader, value, nameof(Loader));
		}

		[Category("Data")]
		[DefaultValue(false)]
		[Description("Monitor file change on file system")]
		public Boolean MonitorFileChange
		{
			get => this._monitorFileChange;
			set => this.SetField(ref this._monitorFileChange, value, nameof(MonitorFileChange));
		}

		[Category("CLI")]
		[Description("Show base MetaTables")]
		[DefaultValue(false)]
		public Boolean ShowBaseMetaTables
		{
			get => this._showBaseMetaTables;
			set => this.SetField(ref this._showBaseMetaTables, value, nameof(ShowBaseMetaTables));
		}

		[Category("Data")]
		//[ReadOnly(true)]
		[Browsable(false)]
		[Description("Loaded files")]
		public String LoadedFilesI
		{
			get => this._loadedFilesI;
			set => this.SetField(ref this._loadedFilesI, value, nameof(LoadedFilesI));
		}

		/// <remarks>.NET 2.0 XML Serializer fix</remarks>
		internal String[] LoadedFiles
		{
			get	=> this.LoadedFilesI == null
				? new String[] { }
				: this.LoadedFilesI.Split(new Char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
			set => this.LoadedFilesI = value == null ? null : String.Join("|", value);
		}

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
		private Boolean SetField<T>(ref T field, T value, String propertyName)
		{
			if(EqualityComparer<T>.Default.Equals(field, value))
				return false;

			field = value;
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			return true;
		}
		#endregion INotifyPropertyChanged
	}
}