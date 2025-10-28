#if NETFRAMEWORK
using System;
using System.Runtime.InteropServices.ComTypes;
using Plugin.PEImageView.Properties;
using TypeLibImporterFlags = System.Runtime.InteropServices.TypeLibImporterFlags;

namespace Plugin.PEImageView.Controls.ResourceControls.TypeLib.Parser
{
	internal static class TypeLibFlagCheck
	{
		public static void ValidateMachineType(TypeLibImporterFlags flags, SYSKIND syskind)
		{
			Int32 multipleCheck = 0;
			if(TypeLibFlagCheck.IsImportingToX86(flags)) multipleCheck++;
			if(TypeLibFlagCheck.IsImportingToX64(flags)) multipleCheck++;
			if(TypeLibFlagCheck.IsImportingToItanium(flags)) multipleCheck++;
			if(TypeLibFlagCheck.IsImportingToAgnostic(flags)) multipleCheck++;
			if(multipleCheck > 1) throw new ArgumentException(Resources.TypeLib_Err_BadMachineSwitch);
			switch(syskind)
			{
			case SYSKIND.SYS_WIN64:
				if(TypeLibFlagCheck.IsImportingToX86(flags)) throw new ArgumentException(Resources.TypeLib_Err_BadMachineSwitch);
				if(TypeLibFlagCheck.IsImportingToDefault(flags)) throw new ArgumentException(Resources.TypeLib_Wrn_AgnosticAssembly);
				break;
			case SYSKIND.SYS_WIN32:
				if(TypeLibFlagCheck.IsImportingToItanium(flags) || TypeLibFlagCheck.IsImportingToX64(flags)) throw new ArgumentException(Resources.TypeLib_Err_BadMachineSwitch);
				break;
			}
		}
		public static Boolean IsImportingToItanium(TypeLibImporterFlags flags) => (flags & TypeLibImporterFlags.ImportAsItanium) != TypeLibImporterFlags.None;
		public static Boolean IsImportingToX64(TypeLibImporterFlags flags) => (flags & TypeLibImporterFlags.ImportAsX64) != TypeLibImporterFlags.None;
		public static Boolean IsImportingToX86(TypeLibImporterFlags flags) => (flags & TypeLibImporterFlags.ImportAsX86) != TypeLibImporterFlags.None;
		public static Boolean IsImportingToAgnostic(TypeLibImporterFlags flags) => (flags & TypeLibImporterFlags.ImportAsAgnostic) != TypeLibImporterFlags.None;
		public static Boolean IsImportingToDefault(TypeLibImporterFlags flags)
			=> !IsImportingToItanium(flags) && !IsImportingToX64(flags) && !IsImportingToX86(flags) && !IsImportingToAgnostic(flags);
	}
}
#else
namespace Plugin.PEImageView.Controls.ResourceControls.TypeLib.Parser
{
	internal static class TypeLibFlagCheck
	{
		public static void ValidateMachineType(System.Runtime.InteropServices.TypeLibImporterFlags flags, System.Runtime.InteropServices.ComTypes.SYSKIND syskind)
		{
			// TypeLibFlagCheck not available in this framework.
		}
	}
}
#endif