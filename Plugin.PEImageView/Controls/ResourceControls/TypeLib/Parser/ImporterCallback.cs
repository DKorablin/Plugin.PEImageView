#if NETFRAMEWORK
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Plugin.PEImageView.Properties;

namespace Plugin.PEImageView.Controls.ResourceControls.TypeLib.Parser
{
	internal class ImporterCallback : ITypeLibImporterNotifySink
	{
		private static TraceSource _trace;
		private readonly ComTypeAnalyzer _analyzer;

		private static TraceSource Trace => ImporterCallback._trace ?? (ImporterCallback._trace = PluginWindows.CreateTraceSource<PluginWindows>(".TlbImp.Callback"));

		public ImporterCallback(ComTypeAnalyzer analyzer) => this._analyzer = analyzer;

		public void ReportEvent(ImporterEventKind eventKind, Int32 eventCode, String eventMsg)
		{
			switch(eventKind)
			{
			case ImporterEventKind.NOTIF_TYPECONVERTED:
				ImporterCallback.Trace.TraceInformation(eventMsg);
				break;
			case ImporterEventKind.NOTIF_CONVERTWARNING:
				ImporterCallback.Trace.TraceEvent(TraceEventType.Warning, 1, eventMsg);
				break;
			}
		}

		public Assembly ResolveRef(Object typeLib)
		{
			ITypeLib typeLibInterface = (ITypeLib)typeLib;
			String typeLibName = Marshal.GetTypeLibName(typeLibInterface);

			ImporterCallback.Trace.TraceInformation(Resources.TypeLib_Msg_ResolvingRefArg1, typeLibName);

			Assembly result = this._analyzer.ReferencedAssemblies[Marshal.GetTypeLibGuid(typeLibInterface)];

			if(result != null)
			{//Looking in the referenced assemblies
				if(!NativeMethods.IsPrimaryInteropAssembly(result))
					throw new ApplicationException(String.Format(Resources.TypeLib_Err_ReferencedPIANotPIAArg1, result.GetName().Name));

				ImporterCallback.Trace.TraceInformation(Resources.TypeLib_Msg_RefFoundInAsmRefListArg2, typeLibName, result.GetName().Name);

				return result;
			}

			if(NativeMethods.GetPrimaryInteropAssembly(typeLibInterface, out String assemblyName, out String assemblyCodeBase))
			{//Looking on the file system
				if(assemblyName != null)
					assemblyName = AppDomain.CurrentDomain.ApplyPolicy(assemblyName);

				try
			{
					result = Assembly.ReflectionOnlyLoad(assemblyName);
				} catch(FileNotFoundException)
				{
					if(assemblyCodeBase == null)
						throw;

					result = Assembly.ReflectionOnlyLoad(assemblyCodeBase);
				} catch(FileLoadException)
				{
					if(assemblyCodeBase == null)
						throw;

					result = Assembly.ReflectionOnlyLoad(assemblyCodeBase);
				}

				if(!NativeMethods.IsPrimaryInteropAssembly(result))
					throw new ApplicationException(String.Format(Resources.TypeLib_Err_RegisteredPIANotPIAArg2, result.GetName().Name, typeLibName));

				ImporterCallback.Trace.TraceInformation(Resources.TypeLib_Msg_ResolvedRefToPIAArg2, typeLibName, result.GetName().Name);
				return result;
			}

			result = this._analyzer.ReferencedAssemblies2[Marshal.GetTypeLibGuid(typeLibInterface)];
			if(result != null)
			{//Looking in the referenced assemblies 2
				ImporterCallback.Trace.TraceInformation(Resources.TypeLib_Msg_AssemblyResolvedArg1, typeLibName);
				return result;
			}

			String assemblyFilePath = Path.Combine(this._analyzer.OutputDir, typeLibName + ".dll");

			try
			{
				result = Assembly.ReflectionOnlyLoadFrom(assemblyFilePath);

				System.Runtime.InteropServices.ComTypes.TYPELIBATTR typeLibAttr = NativeMethods.GetTypeLibAttr(typeLibInterface);

				Version version = result.GetName().Version;
				if(Marshal.GetTypeLibGuidForAssembly(result) == typeLibAttr.guid && ((version.Major == typeLibAttr.wMajorVerNum && version.Minor == typeLibAttr.wMinorVerNum) || (version.Major == 0 && version.Minor == 0 && typeLibAttr.wMajorVerNum == 1 && typeLibAttr.wMinorVerNum == 0)))
				{
					ImporterCallback.Trace.TraceInformation(Resources.TypeLib_Msg_AssemblyLoadedArg1, assemblyFilePath);

					this._analyzer.AlreadyImportedLibraries[Marshal.GetTypeLibGuid(typeLibInterface)] = result;
					return result;
				}

				ImporterCallback.Trace.TraceEvent(TraceEventType.Warning, 10,
					Resources.TypeLib_Msg_AsmRefLookupMatchProblemArg8,
					assemblyFilePath,
					Marshal.GetTypeLibGuidForAssembly(result),
					version.Major,
					version.Minor,
					typeLibName,
					typeLibAttr.guid,
					typeLibAttr.wMajorVerNum,
					typeLibAttr.wMinorVerNum);
			} catch(FileNotFoundException exc)
			{
				ImporterCallback.Trace.TraceEvent(TraceEventType.Information, 10, exc.Message);
			}

			if(this._analyzer.ImportingAssemblies.Contains(Marshal.GetTypeLibGuid(typeLibInterface)))
			{
				ImporterCallback.Trace.TraceEvent(TraceEventType.Warning, 10, Resources.TypeLib_Wrn_CircularReferenceArg1, typeLibName);
				return null;
			}

			return this._analyzer.ImportAssembly(typeLibInterface, assemblyFilePath, null, null);
		}
	}
}
#else
namespace Plugin.PEImageView.Controls.ResourceControls.TypeLib.Parser
{
	internal class ImporterCallback
	{
		public ImporterCallback(ComTypeAnalyzer analyzer) { }
	}
}
#endif