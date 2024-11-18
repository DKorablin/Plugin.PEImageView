using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AlphaOmega.Debug;
using AlphaOmega.Debug.CorDirectory.Meta;
using AlphaOmega.Debug.CorDirectory.Meta.Tables;
using Plugin.PEImageView.Bll;
using Plugin.PEImageView.Controls;
using ComPe = AlphaOmega.Debug.NTDirectory.ComDescriptor;
using System.ComponentModel.Design;

namespace Plugin.PEImageView.Directory
{
	public partial class DocumentMetadata : DocumentBase
	{
		public DocumentMetadata()
			: base(PeHeaderType.DIRECTORY_COM_DECRIPTOR)
		{
			InitializeComponent();
			gridSearch.ListView = lvHeaps;
		}

		protected override void ShowFile(PEFile info)
		{
			lvHeaps.Plugin = base.Plugin;
			this.ShowHeaders(info.ComDescriptor);
		}

		/// <summary>Отобразить все заголовки метаданных</summary>
		/// <param name="directory">.NET директория с метаданными</param>
		private void ShowHeaders(ComPe directory)
		{
			var meta = directory.MetaData;
			Boolean isDirty = tvHierarchy.Nodes.Count > 0;
			foreach(StreamHeader stream in meta)
			{
				TreeNode node = this.GetL1Node(stream);

				switch(stream.Header.Type)
				{
				case Cor.StreamHeaderType.StreamTable:
				case Cor.StreamHeaderType.StreamTableUnoptimized:
					foreach(Cor.MetaTableType tableType in Enum.GetValues(typeof(Cor.MetaTableType)))
						this.GetL2Node(node, tableType, meta.StreamTables);
					break;
				}
			}

			if(isDirty && tvHierarchy.SelectedNode != null)
				this.tvHierarchy_AfterSelect(null, new TreeViewEventArgs(tvHierarchy.SelectedNode));
		}

		private TreeNode GetL1Node(StreamHeader stream)
		{
			foreach(TreeNode node in tvHierarchy.Nodes)
				if(node.Text.Equals(stream.Header.Name, StringComparison.Ordinal))
					return node;

			TreeNode result = new TreeNode(stream.Header.Name) { Tag = stream.Header, };
			tvHierarchy.Nodes.Add(result);
			return result;
		}

		private TreeNode GetL2Node(TreeNode parent, Cor.MetaTableType tableType, StreamTables tables)
		{
			TreeNode result = null;
			foreach(TreeNode node in parent.Nodes)
				if(node.Tag.Equals(tableType))
				{
					result = node;
					break;
				}

			if(result == null)
			{//Такой узел не найден
				result = new TreeNode() { Tag = tableType, };
				parent.Nodes.Add(result);
			}

			//Ставим текст
			UInt32 rowsCount = tables.GetRowsCount(tableType);
			result.Text = $"{tableType} ({rowsCount:n0})";

			//Разукрашиваем
			if(rowsCount == 0)
				result.SetNull();
			else if(result.IsNull())
				result.SetDefaultStyle();

			return result;
		}

		private void tvHierarchy_DragEnter(Object sender, DragEventArgs e)
			=> e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Move : DragDropEffects.None;

		private void tvHierarchy_DragDrop(Object sender, DragEventArgs e)
		{
			String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
			if(files.Length > 0)
				base.OpenFile(files[0]);
		}

		private void tvHierarchy_AfterSelect(Object sender, TreeViewEventArgs e)
		{
			Cor.MetaTableType? tableType = e.Node.Tag as Cor.MetaTableType?;
			Cor.STREAM_HEADER? header = e.Node.Tag as Cor.STREAM_HEADER?;
			List<ListViewItem> items = new List<ListViewItem>();
			var meta = base.GetFile().ComDescriptor.MetaData;

			lvHeaps.Items.Clear();

			try
			{
				base.Cursor = Cursors.WaitCursor;
				if(header.HasValue)
				{//Заголовок потока
					lvHeaps.SuspendLayout();
					lvHeaps.Columns.Clear();
					lvHeaps.Columns.AddRange(new ColumnHeader[] { new ColumnHeader() { Text = "String", }, new ColumnHeader() { Text = "Bin", }, });

					switch(header.Value.Type)
					{
					case Cor.StreamHeaderType.Blob:
						BlobHeap bHeap = meta.BlobHeap;
						foreach(Byte[] b in bHeap.Data)
							items.Add(this.AddHeapItem(b, false));
						break;
					case Cor.StreamHeaderType.Guid:
						GuidHeap gHeap = meta.GuidHeap;
						foreach(Guid g in gHeap.Data)
							items.Add(this.AddHeapItem(g.ToString()));
						break;
					case Cor.StreamHeaderType.StreamTable:
					case Cor.StreamHeaderType.StreamTableUnoptimized:
						StreamTables tHeap = meta.StreamTables;
						break;
					case Cor.StreamHeaderType.String:
						StringHeap sHeap = meta.StringHeap;
						foreach(String str in sHeap.Data)
							items.Add(this.AddHeapItem(str));
						break;
					case Cor.StreamHeaderType.UnicodeSting:
						USHeap usHeap = meta.USHeap;
						foreach(KeyValuePair<Int32,String> str in usHeap.GetDataString())
							items.Add(this.AddHeapItem(str.Value));
						break;
					}
					lvHeaps.Items.AddRange(items.ToArray());
					lvHeaps.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
					lvHeaps.ResumeLayout(false);
				} else if(tableType.HasValue)
				{//Таблица с метаданными
					if(base.Plugin.Settings.ShowBaseMetaTables)
						lvHeaps.DataBind(meta.StreamTables[tableType.Value]);
					else
					{
						Object table = meta.StreamTables.GetType().InvokeMember(tableType.Value.ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, meta.StreamTables, null);
						lvHeaps.DataBind(((IEnumerable)table));
					}
				} else
					throw new NotImplementedException();
			} finally
			{
				base.Cursor = Cursors.Default;
			}

			//Сокрытие ссылок на другие таблицы
			splitDetails.Panel2Collapsed = true;
			foreach(TabPage tab in tabPointers.TabPages)
			{
				foreach(Control tabCtrl in tab.Controls)
					tabCtrl.Dispose();
				tab.Dispose();
			}

			tabPointers.TabPages.Clear();//Удаление всех ранее созданных табов
		}

		private void lvHeaps_SelectedIndexChanged(Object sender, EventArgs e)
		{
			Cor.MetaTableType? tableType = tvHierarchy.SelectedNode.Tag as Cor.MetaTableType?;
			ListViewItem item = lvHeaps.SelectedItems.Count == 1 ? lvHeaps.SelectedItems[0] : null;

			if(tableType.HasValue && !base.Plugin.Settings.ShowBaseMetaTables)
			{
				//Application.DoEvents();
				if(item == null)
					foreach(TabPage tab in tabPointers.TabPages)
						((ListView)tab.Controls[0]).Items.Clear();
				else
				{
					Type rowType = item.Tag.GetType();
					foreach(ColumnHeader column in lvHeaps.Columns)
					{
						Object cell = rowType.InvokeMember(column.Text, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, item.Tag, null);
						CellPointerBase cellPointer = cell as CellPointerBase;
						BaseMetaRow cellRow = cell as BaseMetaRow;
						IEnumerable cellEnum = cell as IEnumerable;
						Byte[] cellByteArray = cell as Byte[];
						if(cellEnum?.GetType().IsBclType() == true)
							continue;//Отсекаю System.String. TODO: Не реботает с Byte[] (cellByteArray)

						if((cellPointer != null && cellPointer.TargetRow != null) || cellEnum != null || cellRow != null || cellByteArray!=null)
						{
							splitDetails.Panel2Collapsed = false;
							Control ctlCellPointer = this.GetOrCreateTabControl(column.Text, cellPointer != null || cellRow != null, cellEnum != null, cellByteArray != null);

							if(cellPointer != null)
							{
								StreamTables tables = base.GetFile().ComDescriptor.MetaData.StreamTables;
								Object baseMetaTable = tables.GetType().InvokeMember(cellPointer.TableType.ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, tables, null);
								Object baseMetaRow = baseMetaTable.GetType().InvokeMember(String.Empty, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, baseMetaTable, new Object[] { cellPointer.RowIndex, });
								((ReflectionListView)ctlCellPointer).DataBind(baseMetaRow);
							} else if(cellRow != null)
								((ReflectionListView)ctlCellPointer).DataBind(cellRow);
							else if(cellEnum != null)
								((ReflectionArrayListView)ctlCellPointer).DataBind(cellEnum);
							else if(cellByteArray != null)
								((ByteViewer)ctlCellPointer).SetBytes(cellByteArray);
							else
								throw new NotSupportedException();
						}
					}
				}
			}
		}

		private ListViewItem AddHeapItem(Byte[] bytes, Boolean useAsciiConversion)
		{
			String str = useAsciiConversion
				? Encoding.ASCII.GetString(bytes)
				: Encoding.Unicode.GetString(bytes);
			return this.AddHeapItem(bytes, str);
		}

		private ListViewItem AddHeapItem(String str)
			=> this.AddHeapItem(Encoding.Unicode.GetBytes(str), str);

		private ListViewItem AddHeapItem(Byte[] bytes, String str)
		{
			ListViewItem item = new ListViewItem();
			String[] subItems = Array.ConvertAll<String, String>(new String[lvHeaps.Columns.Count], delegate(String a) { return String.Empty; });
			item.SubItems.AddRange(subItems);
			item.SubItems[0].Text = str;
			item.SubItems[1].Text = BitConverter.ToString(bytes);
			return item;
			//lvHeaps.Items.Add(item);
		}

		private Control GetOrCreateTabControl(String columnText, Boolean isListView, Boolean isArrayListView, Boolean isByteView)
		{
			TabPage tabColumn = tabPointers.TabPages.Cast<TabPage>().FirstOrDefault(p => p.Text == columnText);
			Control result;

			if(tabColumn == null)
			{
				tabColumn = new TabPage(columnText);

				result = this.CreateTabControl(isListView, isArrayListView, isByteView);
				tabColumn.Controls.Add(result);
				tabPointers.TabPages.Add(tabColumn);
			} else
			{
				result = tabColumn.Controls[0];
				if(isListView && !(result is ReflectionListView)
					|| isArrayListView && !(result is ReflectionArrayListView)
					|| isByteView && !(result is ByteViewer))
				{
					tabColumn.Controls.Clear();
					result = this.CreateTabControl(isListView, isArrayListView, isByteView);
					tabColumn.Controls.Add(result);
				}
			}

			return result;
		}

		private Control CreateTabControl(Boolean isListView, Boolean isArrayListView, Boolean isByteView)
		{
			Control result;
			if(isListView)
				result = new ReflectionListView()
				{
					Plugin = this.Plugin,
					Dock = DockStyle.Fill,
					HeaderStyle = ColumnHeaderStyle.None,
					View = View.Details,
				};
			else if(isArrayListView)
				result = new ReflectionArrayListView()
				{
					Plugin = this.Plugin,
					Dock = DockStyle.Fill,
					HeaderStyle = ColumnHeaderStyle.Clickable,
					Sorting = SortOrder.Ascending,
					View = View.Details,
				};
			else if(isByteView)
				result = new ByteViewer();
			else
				throw new NotSupportedException();

			return result;
		}
	}
}