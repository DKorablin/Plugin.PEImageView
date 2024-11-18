namespace Plugin.PEImageView
{
	public enum PeHeaderType
	{
		/// <summary>DOS Header</summary>
		IMAGE_DOS_HEADER,
		/// <summary>NT32, NT64 Header</summary>
		IMAGE_NT_HEADERS,
		/// <summary>File Header</summary>
		IMAGE_FILE_HEADER,
		/// <summary>COFF File Header</summary>
		IMAGE_COFF_HEADER,
		/// <summary>Optional File header</summary>
		IMAGE_OPTIONAL_HEADER,
		/// <summary>Section descriptor</summary>
		IMAGE_SECTION,
		/// <summary>Sections header</summary>
		IMAGE_SECTION_HEADER,
		DIRECTORY_BOUND_IMPORT,
		DIRECTORY_COM_DECRIPTOR,
		DIRECTORY_DEBUG,
		DIRECTORY_EXPORT,
		DIRECTORY_IMPORT,
		DIRECTORY_LOAD_CONFIG,
		DIRECTORY_RESOURCE,
		DIRECTORY_SECURITY,
		DIRECTORY_RELOCATION,
		DIRECTORY_DELAY_IMPORT,

		DIRECTORY_ARCHITECTURE,
		DIRECTORY_GLOBALPTR,
		/// <summary>Thread Local Storage</summary>
		DIRECTORY_TLS,
		DIRECTORY_IAT,
		DIRECTORY_EXCEPTION,
		DIRECTORY_COR_METADATA,
		DIRECTORY_COR_VTABLE,
		/// <summary>CodeManagerTable</summary>
		DIRECTORY_COR_CMT,
		/// <summary>ExportAddressTable</summary>
		DIRECTORY_COR_EAT,
		/// <summary>ManagedNativeHeaer</summary>
		DIRECTORY_COR_MNH,
		/// <summary>Strong Name signature</summary>
		DIRECTORY_COR_SN,
		/// <summary>Manager resources</summary>
		DIRECTORY_COR_RESOURCE,
	}
}