using System.IO;
using System.Runtime.InteropServices;
using System;
using System.Text;
namespace Global;
public static class WinConsole
{
    public static void Alloc(string? encodingName = null)
    {
        if (Environment.OSVersion.Platform != PlatformID.Win32NT) return;
        Encoding encoding = (encodingName == null) ? encoding = Encoding.Default : CodePagesEncodingProvider.Instance.GetEncoding(encodingName);
        bool consoleAttached = (AllocConsole() != 0);
        var stdout = new StreamWriter(Console.OpenStandardOutput(), encoding);
        stdout.AutoFlush = true;
        Console.SetOut(stdout);
        var stderr = new StreamWriter(Console.OpenStandardError(), encoding);
        stderr.AutoFlush = true;
        Console.SetError(stderr);
    }
    public static void Free()
    {
        if (Environment.OSVersion.Platform != PlatformID.Win32NT) return;
        Console.SetOut(TextWriter.Null);
        Console.SetError(TextWriter.Null);
        FreeConsole();
    }
    #region Win API Functions and Constants
    [DllImport("kernel32.dll",
               EntryPoint = "AllocConsole",
               SetLastError = true,
               CharSet = CharSet.Auto,
               CallingConvention = CallingConvention.StdCall)]
    private static extern int AllocConsole();
    [DllImport("kernel32.dll",
               EntryPoint = "AttachConsole",
               SetLastError = true,
               CharSet = CharSet.Auto,
               CallingConvention = CallingConvention.StdCall)]
    private static extern UInt32 AttachConsole(UInt32 dwProcessId);
    [DllImport("kernel32.dll")]
    private static extern bool FreeConsole();
    private const UInt32 ERROR_ACCESS_DENIED = 5;
    private const UInt32 ATTACH_PARRENT = 0xFFFFFFFF;
    #endregion
}
