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
            Console.WriteLine ("Error:No/Few Arg!");
            Console.WriteLine ("Usage:ExecML CX_HEX_Value DX_HEX_Value ML_HEX ...");
            Console.WriteLine ("Ex:ExecML 80000000 10 0f bc c1 0f bd da 88 dc c3");
            Environment.Exit (1);
        }
        List<byte> bin = new List<byte> ();
        byte[] asm;
        uint CX = Convert.ToUInt32 (argv[0], 16);
        uint DX = Convert.ToUInt32 (argv[1], 16);
        try {
            foreach (string x in argv.Skip (2)) { bin.Add (Convert.ToByte (x, 16)); }
        } catch {
            Environment.Exit (1);
        }
        bin.Add (0xC3); // C3=Ret. for safty. For to Able to run argv only CX,DX
        asm = bin.ToArray ();
        void * buffer = VirtualAlloc (null, asm.Length, 0x1000, 4);
        var func = (delegate * < uint, uint, uint >) buffer;
        int dummy;
        Marshal.Copy (asm, 0, (nint) buffer, asm.Length);
        VirtualProtect (buffer, asm.Length, 0x20, & dummy);
        try {
            unchecked {
                uint r = func (CX, DX);
                Console.WriteLine ($"EAX={r.ToString("X8")}");
            }
            VirtualFree (buffer, 0, 0x8000);
        } finally {
            Environment.Exit (1);
        }
        return 0; // for Warnning
    }
}