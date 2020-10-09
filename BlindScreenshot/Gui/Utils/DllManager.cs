using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Gui.Utils
{
    internal class DllManager
    {
        private const string PathToDllDir = "dll/";
        private const string PathToInjector = "injector.dll";
        private readonly string _pathTox86Dll = Path.GetFullPath("dll/Win32/Dll.dll");
        private readonly string _pathTox64Dll = Path.GetFullPath("dll/x64/Dll.dll");

        [DllImport(PathToInjector, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int inject(string dllPath, uint pid);

        [DllImport(PathToInjector, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int is32Bit(IntPtr handle);

        [DllImport(PathToInjector, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern uint getError();

        public DllManager()
        {
            CheckDllExist();
        }

        public bool Inject(Process process) => Convert.ToBoolean(
          Is32Bit(process)
          ? inject(this._pathTox86Dll, Convert.ToUInt32(process.Id))
          : inject(this._pathTox64Dll, Convert.ToUInt32(process.Id)));

        public uint GetError()
        {
            return getError();
        }

        private static bool Is32Bit(Process process)
        {
            return Convert.ToBoolean(is32Bit(process.Handle));
        }

        private void CheckDllExist()
        {

            if (!Directory.Exists(PathToDllDir))
            {
                throw new Exception("Dll folder not found!");
            }

            if (!File.Exists(this._pathTox86Dll))
            {
                throw new Exception("x86 dll not found!");
            }

            if (!File.Exists(this._pathTox64Dll))
            {
                throw new Exception("x64 dll not found!");
            }

            if (!File.Exists(this._pathTox64Dll))
            {
                throw new Exception("x64 dll not found!");
            }

        }

    }
}
