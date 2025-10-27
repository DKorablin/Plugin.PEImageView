using System.Reflection;
using System.Runtime.InteropServices;

[assembly: Guid("69830948-8609-4691-b5fc-54752102ea62")]
[assembly: System.CLSCompliant(false)]

[assembly: AssemblyDescription("Portable Executable (PE & CLI) image viewer")]
[assembly: AssemblyCopyright("Copyright © Danila Korablin 2012-2025")]

/*if $(ConfigurationName) == Release (
..\..\..\..\ILMerge.exe  "/out:$(ProjectDir)..\bin\$(TargetFileName)" "$(TargetPath)" "$(TargetDir)PEReader.dll" "/lib:..\..\..\SAL\bin"
)*/