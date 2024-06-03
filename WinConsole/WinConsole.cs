using System.IO;
using System.Runtime.InteropServices;
using System;
using System.Text;
using Windows.Win32;
namespace Global;
public static class WinConsole
{
    //public static void Alloc(string? encodingName = null)
    public static void Alloc()
    {
        if (Environment.OSVersion.Platform != PlatformID.Win32NT) return;
        Encoding encoding = CodePagesEncodingProvider.Instance.GetEncoding((int)PInvoke.GetACP());
        PInvoke.AllocConsole();
        var stdout = new StreamWriter(Console.OpenStandardOutput(), encoding);
        stdout.AutoFlush = true;
        Console.SetOut(stdout);
        var stderr = new StreamWriter(Console.OpenStandardError(), encoding);
        stderr.AutoFlush = true;
        Console.SetError(stderr);
        var stdin = new StreamReader(Console.OpenStandardInput(), encoding);
        Console.SetIn(stdin);
    }
    public static void Free()
    {
        if (Environment.OSVersion.Platform != PlatformID.Win32NT) return;
        Console.SetOut(TextWriter.Null);
        Console.SetError(TextWriter.Null);
        PInvoke.FreeConsole();
    }
}
