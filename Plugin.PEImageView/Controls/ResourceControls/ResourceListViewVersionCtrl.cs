using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using AlphaOmega.Debug.NTDirectory.Resources;
using Plugin.PEImageView.Bll;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal class ResourceListViewVersionCtrl : IResourceCtrl
	{
		private readonly ReflectionListView _list;

		public VisualizerType Type => VisualizerType.Version;

		public System.Windows.Forms.Control Control => this._list;

		public ResourceListViewVersionCtrl(PluginWindows plugin)
		{
			this._list = new ReflectionListView
			{
				Plugin = plugin
			};
		}

		public void BindControl(Object data)
		{
			ResourceVersion version = (ResourceVersion)data;
			this._list.Clear();

			List<ListViewItem> items = new List<ListViewItem>();

			//VersionInfo
			var versionInfo = version.VersionInfo;
			Type type = versionInfo.GetType();
			foreach(MemberInfo member in type.GetSearchableMembers())
				items.Add(this._list.CreateReflectedListItem(versionInfo,
					member,
					"VersionInfo",
					() => member.GetMemberValue(versionInfo)));

			//FileInfo
			var fileInfo = version.FileInfo;
			if(fileInfo.HasValue)
			{
				type = fileInfo.GetType();
				foreach(MemberInfo member in type.GetSearchableMembers())
					items.Add(this._list.CreateReflectedListItem(fileInfo.Value,
						member,
						"FileInfo",
						() => member.GetMemberValue(fileInfo.Value)));
			}

			//Tables
			foreach(var tableType in version.GetFileInfo())
			{
				String tableTypeName = tableType.szKey.ToString();
				foreach(var table in tableType.Items)
				{
					String tableName = table.szKey;
					foreach(var row in table.Items)
					{
						String name = row.szKey;
						if(String.IsNullOrEmpty(name))
							name = tableName;
						items.Add(this._list.CreateListItem(row, name, row.ToString(), $"{tableName} ({tableTypeName})", false));
					}
				}
			}
			this._list.Items.AddRange(items.ToArray());
			this._list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}
		public void Dispose()
			=> this._list.Dispose();
	}
}