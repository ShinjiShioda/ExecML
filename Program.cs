using System;
using System.Linq;
using System.Runtime.InteropServices;

public static unsafe class Program {
    [DllImport ("kernel32.dll")]
    private static extern void * VirtualAlloc (void * addr, int size, int type, int protect);
    [DllImport ("kernel32.dll")]
    private static extern bool VirtualProtect (void * addr, int size, int new_protect, int * old_protect);
    [DllImport ("kernel32.dll")]
    private static extern bool VirtualFree (void * addr, int size, int type);

    public static int Main (string[] argv) {
        if (argv.Length < 2) {
            if ((argv.Length == 1) && (argv[0] == "-h" || argv[0] == "-?" | argv[0] == "-help" | argv[0] == "--help")) {
                Console.WriteLine ("HELP");
            } else {
                Console.WriteLine ("Error:Few Arg!");
            }
            Console.WriteLine ("Usage:ExecML RCX_HEX_Value RDX_HEX_Value ML_HEX ...");
            Console.WriteLine ("Ex:ExecML 0x80000000 0x10 0f bc c1 0f bd ca 88 cc c3");
            Environment.Exit (1);
        }
        List<byte> bin = new List<byte> ();
        byte[] asm;
        UInt64 RCX = Convert.ToUInt64 (argv[0], 16);
        UInt64 RDX = Convert.ToUInt64 (argv[1], 16);
        try {
            foreach (string x in argv.Skip (2)) { bin.Add (Convert.ToByte (x, 16)); }
        } catch {
            Console.WriteLine ("Error:Wrong Arg!");
            Environment.Exit (1);
        }
        bin.Add (0xC3); // C3=Ret. for safty. For to Able to run argv only ECX,EDX
        asm = bin.ToArray ();
        void * buffer = VirtualAlloc (null, asm.Length, 0x1000, 4);
        var func = (delegate * < UInt64, UInt64, UInt64 >) buffer;
        int oldProtect;
        Marshal.Copy (asm, 0, (nint) buffer, asm.Length);
        VirtualProtect (buffer, asm.Length, 0x20, & oldProtect);
        try {
            unchecked {
                UInt64 RAX = func (RCX, RDX);
                Console.WriteLine (RAX.ToString("X16"));
            }
            VirtualFree (buffer, 0, 0x8000);
        } finally {
            Environment.Exit (1);
        }
        return 0; // for Warnning
    }
}