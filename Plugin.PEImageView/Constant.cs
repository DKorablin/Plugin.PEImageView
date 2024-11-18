using System;
using Plugin.PEImageView.Properties;

namespace Plugin.PEImageView
{
	internal static class Constant
	{
		/// <summary>Наименование бинарного файла загруженного из памяти</summary>
		public const String BinaryFile = "Binary";

		/// <summary>Получить наименование заголовка</summary>
		/// <param name="type">Тип заголовка</param>
		/// <returns>Наименование заголовка</returns>
		public static String GetHeaderName(PeHeaderType type)
		{
			switch(type)
			{
			case PeHeaderType.IMAGE_DOS_HEADER:			return Resources.Section_IMAGE_DOS_HEADER;
			case PeHeaderType.IMAGE_NT_HEADERS:			return Resources.Section_IMAGE_NT_HEADERS;
			case PeHeaderType.IMAGE_FILE_HEADER:		return Resources.Section_IMAGE_FILE_HEADER;
			case PeHeaderType.IMAGE_COFF_HEADER:		return Resources.Section_IMAGE_COFF_HEADER;
			case PeHeaderType.IMAGE_OPTIONAL_HEADER:	return Resources.Section_IMAGE_OPTIONAL_HEADER;
			case PeHeaderType.IMAGE_SECTION:			return Resources.Section_IMAGE_SECTION;
			case PeHeaderType.IMAGE_SECTION_HEADER:		return Resources.Section_IMAGE_SECTION_HEADER;
			case PeHeaderType.DIRECTORY_BOUND_IMPORT:	return Resources.Section_DIRECTORY_BOUND_IMPORT;
			case PeHeaderType.DIRECTORY_COM_DECRIPTOR:	return Resources.Section_DIRECTORY_COM_DECRIPTOR;
			case PeHeaderType.DIRECTORY_DEBUG:			return Resources.Section_DIRECTORY_DEBUG;
			case PeHeaderType.DIRECTORY_EXPORT:			return Resources.Section_DIRECTORY_EXPORT;
			case PeHeaderType.DIRECTORY_IMPORT:			return Resources.Section_DIRECTORY_IMPORT;
			case PeHeaderType.DIRECTORY_LOAD_CONFIG:	return Resources.Section_DIRECTORY_LOAD_CONFIG;
			case PeHeaderType.DIRECTORY_RESOURCE:		return Resources.Section_DIRECTORY_RESOURCE;
			case PeHeaderType.DIRECTORY_SECURITY:		return Resources.Section_DIRECTORY_SECURITY;
			case PeHeaderType.DIRECTORY_RELOCATION:		return Resources.Section_DIRECTORY_RELOCATION;
			case PeHeaderType.DIRECTORY_DELAY_IMPORT:	return Resources.Section_DIRECTORY_DELAY_IMPORT;

			case PeHeaderType.DIRECTORY_ARCHITECTURE:	return Resources.Section_DIRECTORY_ARCHITECTURE;
			case PeHeaderType.DIRECTORY_GLOBALPTR:		return Resources.Section_DIRECTORY_GLOBALPTR;
			case PeHeaderType.DIRECTORY_TLS:			return Resources.Section_DIRECTORY_TLS;
			case PeHeaderType.DIRECTORY_IAT:			return Resources.Section_DIRECTORY_IAT;
			case PeHeaderType.DIRECTORY_EXCEPTION:		return Resources.Section_DIRECTORY_EXCEPTION;
			case PeHeaderType.DIRECTORY_COR_METADATA:	return Resources.Section_DIRECTORY_COR_METADATA;
			case PeHeaderType.DIRECTORY_COR_RESOURCE:	return Resources.Section_DIRECTORY_COR_RESOURCE;
			case PeHeaderType.DIRECTORY_COR_VTABLE:		return Resources.Section_DIRECTORY_COR_VTABLE;
			case PeHeaderType.DIRECTORY_COR_CMT:		return Resources.Section_DIRECTORY_COR_CMT;
			case PeHeaderType.DIRECTORY_COR_EAT:		return Resources.Section_DIRECTORY_COR_EAT;
			case PeHeaderType.DIRECTORY_COR_MNH:		return Resources.Section_DIRECTORY_COR_MNH;
			case PeHeaderType.DIRECTORY_COR_SN:			return Resources.Section_DIRECTORY_COR_SN;
			default:
				throw new NotSupportedException();
			}
		}
	}
}