using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using Plugin.PEImageView.Controls.ResourceControls.TypeLib.Parser;

namespace Plugin.PEImageView
{
	internal static class NativeMethods
	{
		/// <summary>The CRYPTUI_VIEWCERTIFICATE_STRUCT structure contains information about a certificate to view. This structure is used in the CryptUIDlgViewCertificate function.</summary>
		private struct CRYPTUI_VIEWCERTIFICATE_STRUCT
		{
			/// <summary>The size, in bytes, of the CRYPTUI_VIEWCERTIFICATE_STRUCT structure.</summary>
			public Int32 dwSize;
			/// <summary>A handle to the window that is the parent of the dialog box produced by CryptUIDlgViewCertificate.</summary>
			public IntPtr hwndParent;
			/// <summary>Flags</summary>
			public CRYPTUI_FLAGS dwFlags;
			/// <summary>A pointer to a null-terminated string that contains the title for the window.</summary>
			[MarshalAs(UnmanagedType.LPWStr)]
			public String szTitle;
			/// <summary>A pointer to the CERT_CONTEXT structure that contains the certificate context to display.</summary>
			public IntPtr pCertContext;
			/// <summary>An array of pointers to null-terminated strings that contain the purposes for which this certificate will be validated.</summary>
			public IntPtr rgszPurposes;
			/// <summary>The number of purposes in the rgszPurposes array.</summary>
			public Int32 cPurposes;
			/// <summary>
			/// If the WinVerifyTrust function has already been called for the certificate and the WTHelperProvDataFromStateData function was also called,
			/// pass in a pointer to the state structure that was acquired from the call to WTHelperProvDataFromStateData.
			/// If pCryptProviderData is set, fpCryptProviderDataTrustedUsage, idxSigner, idxCert, and fCounterSignature must also be set.
			/// </summary>
			public IntPtr pCryptProviderData; // or hWVTStateData
			/// <summary>If WinVerifyTrust was called, this is the result of whether the certificate was trusted.</summary>
			public Boolean fpCryptProviderDataTrustedUsage;
			/// <summary>The index of the signer to view.</summary>
			public Int32 idxSigner;
			/// <summary>The index of the certificate that is being viewed within the signer chain. The certificate context of this cert must match pCertContext.</summary>
			public Int32 idxCert;
			/// <summary>TRUE if a countersignature is being viewed. If this is TRUE, idxCounterSigner must be valid.</summary>
			public Boolean fCounterSigner;
			/// <summary>The index of the countersigner to view.</summary>
			public Int32 idxCounterSigner;
			/// <summary>The number of other stores in the rghStores array of certificate stores to search when building and validating the certificate chain.</summary>
			public Int32 cStores;
			/// <summary>An array of HCERTSTORE handles to other certificate stores to search when building and validating the certificate chain.</summary>
			public IntPtr rghStores;
			/// <summary>The number of property pages to add to the dialog box.</summary>
			public Int32 cPropSheetPages;
			/// <summary>
			/// An array of property pages to add to the dialog box.
			/// Each page in this array will not receive the lParam in the PROPSHEETPAGE structure as the lParam in the WM_INITDIALOG message.
			/// It will instead receive a pointer to a CRYPTUI_INITDIALOG_STRUCT structure.
			/// It contains the lParam in PROPSHEETPAGE and the pointer to the CERT_CONTEXT for which the page is being displayed.
			/// </summary>
			public IntPtr rgPropSheetPages;
			/// <summary>
			/// The index of the initial page that will be displayed.
			/// If the highest bit (0x8000) is set, the index is assumed to index rgPropSheetPages (after the highest bit has been stripped off, for example, 0x8000 will indicate the first page in rgPropSheetPages).
			/// If the highest bit is zero, nStartPage will be the starting index of the default certificate dialog box property pages.
			/// </summary>
			public Int32 nStartPage;
		}
		[DllImport("CryptUI.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern Boolean CryptUIDlgViewCertificate(ref CRYPTUI_VIEWCERTIFICATE_STRUCT pCertViewInfo, ref Boolean pfPropertiesChanged);

		[Flags]
		private enum CRYPTUI_FLAGS
		{
			/// <summary>The Certification Path page is disabled.</summary>
			CRYPTUI_HIDE_HIERARCHYPAGE = 0x00000001,
			/// <summary>The Details page is disabled.</summary>
			CRYPTUI_HIDE_DETAILPAGE = 0x00000002,
			/// <summary>The user is not allowed to change the properties.</summary>
			CRYPTUI_DISABLE_EDITPROPERTIES = 0x00000004,
			/// <summary>The user is allowed to change the properties.</summary>
			CRYPTUI_ENABLE_EDITPROPERTIES = 0x00000008,
			/// <summary>The Install button is disabled.</summary>
			CRYPTUI_DISABLE_ADDTOSTORE = 0x00000010,
			/// <summary>The Install button is enabled.</summary>
			CRYPTUI_ENABLE_ADDTOSTORE = 0x00000020,
			/// <summary>The pages or buttons that allow the user to accept or decline any decision are disabled.</summary>
			CRYPTUI_ACCEPT_DECLINE_STYLE = 0x00000040,
			/// <summary>An untrusted root error is ignored.</summary>
			CRYPTUI_IGNORE_UNTRUSTED_ROOT = 0x00000080,
			/// <summary>Known trusted stores will not be used to build the chain.</summary>
			CRYPTUI_DONT_OPEN_STORES = 0x00000100,
			/// <summary>A known trusted root store will not be used to build the chain.</summary>
			CRYPTUI_ONLY_OPEN_ROOT_STORE = 0x00000200,
			/// <summary>
			/// Use only when viewing certificates on remote computers.
			/// If this flag is used, the first element of rghStores must be the handle of the root store on the remote computer.
			/// </summary>
			CRYPTUI_WARN_UNTRUSTED_ROOT = 0x00000400,
			/// <summary>
			/// Enable revocation checking with default behavior.
			/// The default behavior is to enable revocation checking of the entire certificate chain except the root certificate.
			/// Valid only if neither the pCryptProviderData nor the hWVTStateData union member is passed in.
			/// </summary>
			CRYPTUI_ENABLE_REVOCATION_CHECKING = 0x00000800,
			/// <summary>When building a certificate chain for a remote computer, warn that the chain may not be trusted on the remote computer.</summary>
			CRYPTUI_WARN_REMOTE_TRUST = 0x00001000,
			/// <summary>If this flag is set, the Copy to file button will be disabled on the Details tab.</summary>
			CRYPTUI_DISABLE_EXPORT = 0x00002000,
			/// <summary>
			/// Enable revocation checking only on the leaf certificate in the certificate chain.
			/// Valid only if neither the pCryptProviderData nor the hWVTStateData union member is passed in.
			/// </summary>
			CRYPTUI_ENABLE_REVOCATION_CHECK_END_CERT = 0x00004000,
			/// <summary>
			/// Enable revocation checking on each certificate in the certificate chain.
			/// Valid only if neither the pCryptProviderData nor the hWVTStateData union member is passed in.
			/// </summary>
			/// <remarks>
			/// Because root certificates rarely contain information that allows revocation checking, it is expected that use of this option will usually result in failure of the CryptUIDlgViewCertificate function.
			/// The recommended option is to use CRYPTUI_ENABLE_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT
			/// </remarks>
			CRYPTUI_ENABLE_REVOCATION_CHECK_CHAIN = 0x00008000,
		}

		public static Boolean ViewCertificate(X509Certificate2 certificate)
		{
			CRYPTUI_VIEWCERTIFICATE_STRUCT certViewInfo = new CRYPTUI_VIEWCERTIFICATE_STRUCT()
			{
				dwSize = Marshal.SizeOf(typeof(CRYPTUI_VIEWCERTIFICATE_STRUCT)),
				pCertContext = certificate.Handle,
				szTitle = "Certificate",
				dwFlags = CRYPTUI_FLAGS.CRYPTUI_DISABLE_ADDTOSTORE,
				nStartPage = 0,
			};
			Boolean fPropertiesChanged = false;
			Boolean result = NativeMethods.CryptUIDlgViewCertificate(ref certViewInfo, ref fPropertiesChanged);
			/*if(!result)
				throw new Win32Exception();*/

			return result;
		}
		[DllImport("User32.dll")]
		public static extern Int64 GetDialogBaseUnits();

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public Int32 left, top, right, bottom;
		}
		[DllImport("user32.dll", SetLastError = true)]
		public static extern Boolean MapDialogRect(IntPtr hDlg, ref RECT lpRect);

		#region TypeLib
		[Flags]
		public enum REGKIND
		{
			DEFAULT = 0,
			REGISTER = 1,
			NONE = 2,
			LOAD_TLB_AS_32BIT = 32,
			LOAD_TLB_AS_64BIT = 64
		}

		private static TypeLibConverter _TypeLibConverter = new TypeLibConverter();

		public static ITypeLib LoadTypeLib(String filePath, TypeLibImporterFlags flags)
		{
			ITypeLib result;
			REGKIND regKind = REGKIND.NONE;
			if(Environment.OSVersion.Platform != PlatformID.Win32Windows)
			{
				if(TypeLibFlagCheck.IsImportingToItanium(flags) || TypeLibFlagCheck.IsImportingToX64(flags))
					regKind |= REGKIND.LOAD_TLB_AS_64BIT;
				else if(TypeLibFlagCheck.IsImportingToX86(flags))
					regKind |= REGKIND.LOAD_TLB_AS_32BIT;
			}

			try
			{
				NativeMethods.LoadTypeLibEx(filePath, regKind, out result);
			} catch(COMException)
			{
				//exc.ErrorCode == -2147312566;
				throw;
			}

			return result;
		}

		public static Boolean IsPrimaryInteropAssembly(Assembly assembly)
		{
			foreach(CustomAttributeData attribute in CustomAttributeData.GetCustomAttributes(assembly))
				if(attribute.Constructor.DeclaringType == typeof(PrimaryInteropAssemblyAttribute))
					return true;

			return false;
		}

		public static Boolean GetPrimaryInteropAssembly(ITypeLib typeLib, out String assemblyName, out String assemblyCodeBase)
		{
			System.Runtime.InteropServices.ComTypes.TYPELIBATTR typeLibAttr = NativeMethods.GetTypeLibAttr(typeLib);

			return NativeMethods._TypeLibConverter.GetPrimaryInteropAssembly(
				typeLibAttr.guid,
				typeLibAttr.wMajorVerNum,
				typeLibAttr.wMinorVerNum,
				typeLibAttr.lcid,
				out assemblyName,
				out assemblyCodeBase);
		}

		/// <summary>Получить атрибуты COM+ библиотеки</summary>
		/// <param name="typeLib">Указатель на COM+ библиотеку</param>
		/// <returns>Заголовок COM+ библиотеки</returns>
		public static System.Runtime.InteropServices.ComTypes.TYPELIBATTR GetTypeLibAttr(ITypeLib typeLib)
		{
			IntPtr ptrLibAttr = IntPtr.Zero;

			try
			{
				typeLib.GetLibAttr(out ptrLibAttr);
				return (System.Runtime.InteropServices.ComTypes.TYPELIBATTR)Marshal.PtrToStructure(ptrLibAttr, typeof(System.Runtime.InteropServices.ComTypes.TYPELIBATTR));
			} finally
			{
				if(ptrLibAttr != IntPtr.Zero)
					typeLib.ReleaseTLibAttr(ptrLibAttr);
			}
		}

		/// <summary>Loads a type library and (optionally) registers it in the system registry</summary>
		/// <remarks>Enables programmers to specify whether or not the type library should be loaded</remarks>
		/// <param name="strTypeLibName">The type library file</param>
		/// <param name="regKind">
		/// Identifies the kind of registration to perform for the type library based on the following flags: DEFAULT, REGISTER and NONE.
		/// REGKIND_DEFAULT simply calls LoadTypeLib and registration occurs based on the LoadTypeLib registration rules.
		/// REGKIND_NONE calls LoadTypeLib without the registration process enabled.
		/// REGKIND_REGISTER calls LoadTypeLib followed by RegisterTypeLib, which registers the type library.
		/// To unregister the type library, use UnRegisterTypeLib.
		/// </param>
		/// <param name="TypeLib">The type library</param>
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
		private static extern void LoadTypeLibEx(String strTypeLibName, REGKIND regKind, out ITypeLib TypeLib);
		#endregion TypeLib
	}
}