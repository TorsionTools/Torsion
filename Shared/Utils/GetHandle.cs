using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Torsion.Utils
{
    /// <summary>
    /// Class to provide HWND handles for Windows
    /// </summary>
    internal class GetHandle : IWin32Window
    {
        internal GetHandle(IntPtr handle)
        {
            Debug.Assert(handle != IntPtr.Zero, "No Handle");
            Handle = handle;
        }
        public IntPtr Handle { get; }
    }
}
