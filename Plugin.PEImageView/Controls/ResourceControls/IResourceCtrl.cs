using System;
using System.Windows.Forms;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	/// <summary>Отображения элемента ресурсов</summary>
	internal interface IResourceCtrl : IDisposable
	{
		/// <summary>Тип элемента управления</summary>
		VisualizerType Type { get; }
		/// <summary>Пользовательский элемент управления</summary>
		Control Control { get; }
		/// <summary>Применить данные к элементу упрвления</summary>
		/// <param name="data">Данные</param>
		/// <param name="index">Индекс из <see cref="T:SelectableData"/> или null</param>
		void BindControl(Object data);
	}
}