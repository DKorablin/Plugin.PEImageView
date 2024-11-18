using System.Reflection;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: Guid("69830948-8609-4691-b5fc-54752102ea62")]
[assembly: System.CLSCompliant(false)]

#if NETCOREAPP
[assembly: AssemblyMetadata("ProjectUrl", "https://dkorablin.ru/project/Default.aspx?File=89")]
#else

[assembly: AssemblyTitle("Plugin.PEImageView")]
[assembly: AssemblyDescription("Portable Executable (PE & CLI) image viewer")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("Danila Korablin")]
[assembly: AssemblyProduct("Plugin.PEImageView")]
[assembly: AssemblyCopyright("Copyright © Danila Korablin 2012-2024")]
#endif

/*if $(ConfigurationName) == Release (
..\..\..\..\ILMerge.exe  "/out:$(ProjectDir)..\bin\$(TargetFileName)" "$(TargetPath)" "$(TargetDir)PEReader.dll" "/lib:..\..\..\SAL\bin"
)*/