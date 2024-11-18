using System;

namespace Plugin.PEImageView.Controls.ResourceControls
{
	/// <summary>Интерфейс поддерживающий выбор индекса данных</summary>
	internal interface IResourceSelectorCtrl : IResourceCtrl
	{
		/// <summary>Данные, который должен выбрать пользователь перед отображением</summary>
		String[] SelectableData { get; }
		/// <summary>Выбрать данные по индексу</summary>
		/// <param name="index">Индекс данных</param>
		void SelectData(Int32 index);
	}
}