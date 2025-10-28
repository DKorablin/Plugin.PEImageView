using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Plugin.PEImageView.Bll;

namespace Plugin.PEImageView.Source
{
	internal partial class ProcessDlg : Form
	{
		internal List<String> _selectedFiles = new List<String>();
		public String[] SelectedFiles => this._selectedFiles.ToArray();

		public ProcessDlg(PluginWindows plugin)
		{
			InitializeComponent();
			lvInfo.Plugin = plugin;
			this.processTimer_Tick(this, EventArgs.Empty);
		}

		private void processTimer_Tick(Object sender, EventArgs e)
		{
			processTimer.Stop();
			Process[] processes = Process.GetProcesses();
			for(Int32 loop = tvProcess.Nodes.Count - 1;loop >= 0;loop--)
			{//Deleting terminated processes
				TreeNode node = tvProcess.Nodes[loop];
				Boolean found = false;
				foreach(Process process in processes)
					if(((Int32)node.Tag) == process.Id)
					{
						found = true;
						break;
					}

				if(!found)
				{
					if(tvProcess.SelectedNode == node)
						lvInfo.Items.Clear();
					tvProcess.Nodes.RemoveAt(loop);
				}
			}
			foreach(Process process in processes)
			{//Adding new processes
				Boolean found = false;
				foreach(TreeNode node in tvProcess.Nodes)
					if(((Int32)node.Tag) == process.Id)
					{
						found = true;
						break;
					}
				if(!found)
					tvProcess.Nodes.Add(CreateProcessNode(process));
			}
			processTimer.Start();
		}

		private void tvProcess_AfterSelect(Object sender, TreeViewEventArgs e)
		{
			if(IsProcessNode(e.Node))//Process
				lvInfo.DataBind(GetProcess(e.Node));
			else if(IsModuleNode(e.Node))//Module
				lvInfo.DataBind(GetProcessModule(e.Node));
		}
		private void tvProcess_BeforeExpand(Object sender, TreeViewCancelEventArgs e)
		{
			if(e.Action == TreeViewAction.Expand
				&& IsProcessNode(e.Node)
				&& e.Node.Nodes.Count == 1
				&& String.IsNullOrEmpty(e.Node.Nodes[0].Text))
			{
				e.Node.Nodes.Clear();
				Process process = GetProcess(e.Node);
				foreach(ProcessModule module in process.Modules)
				{
					TreeNode node = new TreeNode(module.ModuleName) { Tag = module.FileName, };
					e.Node.Nodes.Add(node);
				}
			}
		}

		private void tvProcess_BeforeCheck(Object sender, TreeViewCancelEventArgs e)
		{
			if(e.Node.IsException())
				e.Cancel = true;
			else
			{
				String fileName;
				ProcessModule module = null;
				if(IsProcessNode(e.Node))
					module = TryGetProcessModule(GetProcess(e.Node));
				else if(IsModuleNode(e.Node))
					module = GetProcessModule(e.Node);
				else
					e.Cancel = true;

				if(module != null)
				{
					fileName = module.FileName;
					if(e.Node.Checked)
						this._selectedFiles.Remove(fileName);
					else
						this._selectedFiles.Add(fileName);
					bnOk.Enabled = this._selectedFiles.Count > 0;
				}
			}
		}

		private static TreeNode CreateProcessNode(Process process)
		{
			Boolean error = ProcessDlg.TryGetProcessModuleName(process, out String processName);
			TreeNode result = new TreeNode($"{processName} ({process.Id})") { Tag = process.Id, ForeColor = error ? NodeExtender.ExceptionColor : Color.Empty, };
			if(!error)
				result.Nodes.Add(String.Empty);
			return result;
		}

		private static Boolean TryGetProcessModuleName(Process process,out String moduleName)
		{
			ProcessModule module = TryGetProcessModule(process);
			if(module == null)
			{
				moduleName = process.ProcessName;
				return true;
			} else
			{
				moduleName = module.ModuleName;
				return false;
			}
		}

		private static ProcessModule TryGetProcessModule(Process process)
		{
			try
			{
				return process.MainModule;
			} catch(Win32Exception)
			{//Access is denied
				return null;
			}
		}

		private static Boolean IsProcessNode(TreeNode node)
			=> node.Parent == null;

		private static Boolean IsModuleNode(TreeNode node)
			=> node.Parent != null && node.Parent.Parent == null;

		private static Process GetProcess(TreeNode node)
		{
			if(node == null)
				return null;
			else if(IsProcessNode(node))//Process
				return Process.GetProcessById((Int32)node.Tag);
			else if(IsModuleNode(node))
				return Process.GetProcessById((Int32)node.Parent.Tag);
			else
				return null;
		}

		private static ProcessModule GetProcessModule(TreeNode node)
		{
			String moduleName = node.Text;
			foreach(ProcessModule module in GetProcess(node).Modules)
				if(module.ModuleName == moduleName)
					return module;
			return null;
		}
	}
}