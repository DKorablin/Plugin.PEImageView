using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using AlphaOmega.Reflection;

namespace Plugin.PEImageView.Source
{
	internal partial class GacBrowserDlg : Form
	{
		/// <summary>Результат</summary>
		public enum ResultType
		{
			/// <summary>Наименование сборки</summary>
			AssemblyName,
			/// <summary>Путь к сборке</summary>
			FilePath,
		}

		/// <summary>Вкладки на форме</summary>
		private enum TabPageName
		{
			GAC = 0,
			Browse = 1,
		}

		/// <summary>Выбранная вкладка</summary>
		private TabPageName SelectedTab
		{
			get => (TabPageName)tabMain.SelectedIndex;
			set => tabMain.SelectedIndex = (Int32)value;
		}

		public GacBrowserDlg()
		{
			InitializeComponent();
			gridSearch.ListView = lvGac;
			this.tabMain_SelectedIndexChanged(this, EventArgs.Empty);
		}

		protected override void OnShown(EventArgs e)
		{
			gridSearch.Visible = false;
			base.OnShown(e);
		}

		public IEnumerable<String> GetSelectedAssemblies(ResultType type)
		{
			switch(this.SelectedTab)
			{
			case TabPageName.GAC:
				Int32 pathIndex = GacBrowserDlg.GetColumnIndex(lvGac, "Path");
				foreach(ListViewItem item in lvGac.SelectedItems)
					switch(type)
					{
					case ResultType.AssemblyName:
						yield return (String)item.Tag;//Assembly Name
						break;
					case ResultType.FilePath:
						yield return item.SubItems[pathIndex].Text;//Путь к сборке на диске
						break;
					default:
						throw new NotImplementedException();
					}
				//return result;
				break;
			case TabPageName.Browse:
				foreach(ListViewItem item in lvBrowse.SelectedItems)
					switch(type)
					{
					case ResultType.AssemblyName:
						yield return AssemblyName.GetAssemblyName((String)item.Tag).FullName;
						break;
					case ResultType.FilePath:
						yield return (String)item.Tag;
						break;
					default:
						throw new NotImplementedException();
					}
				break;
			default:
				throw new NotImplementedException(String.Format("Index: {0} Name: {1}", tabMain.SelectedIndex, tabMain.SelectedTab.Text));
			}
		}

		private void tabMain_SelectedIndexChanged(Object sender, EventArgs e)
		{
			switch(this.SelectedTab)
			{
			case TabPageName.GAC:
				if(lvGac.Items.Count == 0 && !bgGac.IsBusy)
				{
					lvGac.Items.Clear();
					lvGac.Columns.Clear();
					base.Cursor = Cursors.WaitCursor;
					bgGac.RunWorkerAsync();
				}
				break;
			case TabPageName.Browse:
				break;
			default:
				throw new NotImplementedException(String.Format("Index: {0} Name: {1}", tabMain.SelectedIndex, tabMain.SelectedTab.Text));
			}
		}

		private void bgGac_DoWork(Object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			AssemblyCacheEnum asmCache = new AssemblyCacheEnum();
			List<ListViewItem> itemsToAdd = new List<ListViewItem>();
			foreach(String displayName in asmCache)
			{
				String[] properties = displayName.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				if(properties.Length > 0)
				{
					Int32 index;
					ListViewItem item = new ListViewItem() { Tag = displayName, };

					foreach(String asmProperty in properties)
					{
						String[] nameValue = asmProperty.Split(new Char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
						switch(nameValue.Length)
						{
							case 1://Наименование сборки
								index = GetColumnIndex(lvGac, "Name");
								item.Text = nameValue[0];//Добавлюя колонку наименования сборки
								break;
							case 2://Параметры сборки
								index = GetColumnIndex(lvGac, nameValue[0].Trim());

								while(lvGac.Columns.Count > item.SubItems.Count)//Добавляю колонки к ряду
									item.SubItems.Add(String.Empty);

								item.SubItems[index].Text = nameValue[1];
								break;
							default://ХЗ
								throw new NotImplementedException(String.Format("Can't format assembly '{0}' parameter '{1}'", displayName, asmProperty));
						}
					}

					String path = AssemblyCache.QueryAssemblyInfo(displayName);
					index = GetColumnIndex(lvGac, "Path");

					while(lvGac.Columns.Count > item.SubItems.Count)//Добавляю колонки к ряду
						item.SubItems.Add(String.Empty);

					item.SubItems[index].Text = path;

					itemsToAdd.Add(item);
				}
			}
			lvGac.Invoke((MethodInvoker)delegate { lvGac.Items.AddRange(itemsToAdd.ToArray()); });
		}

		void bgGac_RunWorkerCompleted(Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			base.Cursor = Cursors.Default;
			lvGac.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

		/// <summary>Получить индекс добавленной или созданной ранее колонки</summary>
		/// <param name="columnName">Наименование добавленной или созданной ранее колонки</param>
		/// <returns>Индекс колонки</returns>
		private static Int32 GetColumnIndex(ListView lv, String columnName)
		{
			foreach(ColumnHeader columnItem in lv.Columns)
				if(columnItem.Text.Equals(columnName))
					return columnItem.Index;

			if(lv.InvokeRequired)
				return (Int32)lv.Invoke((Func<Int32>)delegate { return lv.Columns.Add(new ColumnHeader() { Text = columnName, }); });
			else
				return lv.Columns.Add(new ColumnHeader() { Text = columnName, });
		}

		private void cmsBrowse_Opening(Object sender, System.ComponentModel.CancelEventArgs e)
			=> tsmiRemove.Visible = lvBrowse.SelectedItems.Count > 0;

		private void cmsBrowse_ItemClicked(Object sender, ToolStripItemClickedEventArgs e)
		{
			if(e.ClickedItem == tsmiBrowse)
			{
				using(OpenFileDialog dlg = new OpenFileDialog() { CheckFileExists = true, Multiselect = true, Filter = "Assemblies (*.dll)|*.dll|All files (*.*)|*.*", Title = "Add reference", })
					if(dlg.ShowDialog() == DialogResult.OK)
					{
						foreach(String filePath in dlg.FileNames)
						{
							String path = Path.GetDirectoryName(filePath);
							ListViewGroup pathGroup = null;
							foreach(ListViewGroup group in lvBrowse.Groups)
								if(group.Header.Equals(path))
									pathGroup = group;
							if(pathGroup == null)
								pathGroup = lvBrowse.Groups.Add(String.Empty, path);
							lvBrowse.Items.Add(new ListViewItem(Path.GetFileName(filePath)) { Tag = filePath, Group = pathGroup, });
						}
						lvBrowse.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
					}
			} else if(e.ClickedItem == tsmiRemove)
			{
				if(lvBrowse.SelectedItems.Count > 0)
					if(MessageBox.Show("Are you sure you want to remove selected assembly from list?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						while(lvBrowse.SelectedItems.Count > 0)
							lvBrowse.SelectedItems[0].Remove();
			} else throw new NotImplementedException(String.Format("Sender: {0} ClickedItem: {1}", sender, e.ClickedItem));
		}
	}
}