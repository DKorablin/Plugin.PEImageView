using System;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	internal enum VisualizerType
	{
		/// <summary>Нет визуализации</summary>
		None,
		/// <summary>Визуализация в виде списка</summary>
		ListView,
		/// <summary>Визуализация в браузере</summary>
		WebBrowser,
		/// <summary>Визуализация в виде списка создаваемого через рефлексию</summary>
		ListViewArray,
		/// <summary>Визуализация в виде таблицы</summary>
		GridView,
		/// <summary>Диалог</summary>
		Dialog,
		/// <summary>Меню</summary>
		Menu,
		/// <summary>Тулбар</summary>
		ToolBar,
		/// <summary>Версия</summary>
		Version,
		/// <summary>Массив байт</summary>
		BinView,
		/// <summary>COM+</summary>
		TypeLib,
		/// <summary>Картинка</summary>
		Bitmap,
	}
}