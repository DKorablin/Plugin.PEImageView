#if NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Plugin.PEImageView.Controls.ResourceControls.TypeLib.Parser
{
	internal class ComTypeAnalyzer
	{
		private static readonly TypeLibConverter _typeLibConverter = new TypeLibConverter();

		public Dictionary<Guid, Assembly> ReferencedAssemblies { get; } = new Dictionary<Guid, Assembly>();
		public Dictionary<Guid, Assembly> ReferencedAssemblies2 { get; } = new Dictionary<Guid, Assembly>();
		public Dictionary<Guid, Assembly> AlreadyImportedLibraries { get; } = new Dictionary<Guid, Assembly>();
		public List<Guid> ImportingAssemblies { get; } = new List<Guid>();

		public String InputFile { get; }
		public TypeLibImporterFlags Flags { get; }
		public String OutputDir { get; }

		public ComTypeAnalyzer(String inputFile, TypeLibImporterFlags flags, String outputDir)
		{
			this.Flags = flags;
			this.InputFile = inputFile;
			this.OutputDir = outputDir;
		}

		public Assembly ImportAssembly()
		{
			ITypeLib typeLib = NativeMethods.LoadTypeLib(this.InputFile, this.Flags);
			String typeLibFileName = Marshal.GetTypeLibName(typeLib) + ".dll";

			return this.ImportAssembly(typeLib, typeLibFileName, null, null);
		}

		public Assembly ImportAssembly(ITypeLib typeLib, String assemblyFileName, String assemblyNamespace, Version version)
		{
			Guid typeLibGuid = Marshal.GetTypeLibGuid(typeLib);

			if(this.AlreadyImportedLibraries.TryGetValue(typeLibGuid, out Assembly result))
				return result;

			this.ImportingAssemblies.Add(typeLibGuid);

			System.Runtime.InteropServices.ComTypes.TYPELIBATTR typeLibAttr = NativeMethods.GetTypeLibAttr(typeLib);
			TypeLibFlagCheck.ValidateMachineType(this.Flags, typeLibAttr.syskind);//TODO: Move to .ctor

			ImporterCallback notifySink = new ImporterCallback(this);

			AssemblyBuilder assemblyBuilder = ComTypeAnalyzer._typeLibConverter.ConvertTypeLibToAssembly(typeLib,
				assemblyFileName,
				this.Flags,
				notifySink,
				null,
				null,
				assemblyNamespace,
				version);

			this.ImportingAssemblies.Remove(typeLibGuid);

			/*String fileName = Path.GetFileName(assemblyFilePath);
			File.Delete(fileName);
			if(TypeLibFlagCheck.IsImportingToX64(flags))
				assemblyBuilder.Save(fileName, PortableExecutableKinds.ILOnly | PortableExecutableKinds.PE32Plus, ImageFileMachine.AMD64);
			else if(TypeLibFlagCheck.IsImportingToItanium(flags))
				assemblyBuilder.Save(fileName, PortableExecutableKinds.ILOnly | PortableExecutableKinds.PE32Plus, ImageFileMachine.IA64);
			else if(TypeLibFlagCheck.IsImportingToX86(flags))
				assemblyBuilder.Save(fileName, PortableExecutableKinds.ILOnly | PortableExecutableKinds.Required32Bit, ImageFileMachine.I386);*/

			this.AlreadyImportedLibraries[typeLibGuid] = assemblyBuilder;

			return assemblyBuilder;
		}
	}
}
#else
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices; // Added for placeholder TypeLibImporterFlags
using System.Runtime.InteropServices.ComTypes;

// Placeholder for missing TypeLibImporterFlags in non-NETFRAMEWORK targets; only minimal value needed.
namespace System.Runtime.InteropServices
{
	[Flags]
	internal enum TypeLibImporterFlags
	{
		None = 0,
	}
}

namespace Plugin.PEImageView.Controls.ResourceControls.TypeLib.Parser
{
	internal class ComTypeAnalyzer
	{
		public Dictionary<Guid, Assembly> ReferencedAssemblies { get; } = new Dictionary<Guid, Assembly>();
		public Dictionary<Guid, Assembly> ReferencedAssemblies2 { get; } = new Dictionary<Guid, Assembly>();
		public Dictionary<Guid, Assembly> AlreadyImportedLibraries { get; } = new Dictionary<Guid, Assembly>();
		public List<Guid> ImportingAssemblies { get; } = new List<Guid>();

		public string InputFile { get; }
		public TypeLibImporterFlags Flags { get; }
		public string OutputDir { get; }

		public ComTypeAnalyzer(string inputFile, TypeLibImporterFlags flags, string outputDir)
		{
			InputFile = inputFile;
			Flags = flags;
			OutputDir = outputDir;
		}

		public Assembly ImportAssembly()
			=> throw new PlatformNotSupportedException("Runtime TypeLib import is only supported on .NET Framework.");
		public Assembly ImportAssembly(ITypeLib typeLib, String assemblyFileName, String assemblyNamespace, Version version)
			=> throw new PlatformNotSupportedException("Runtime TypeLib import is only supported on .NET Framework.");
	}
}
#endif