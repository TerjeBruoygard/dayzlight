using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DayzlightAddonTests
{
    public class AddonWrapper
    {
        [DllImport("DayzlightAddon.dll", EntryPoint = "_RVExtension@12", CallingConvention = CallingConvention.Winapi)]
        public static extern void RVExtension(StringBuilder output, int outputSize, [MarshalAs(UnmanagedType.LPStr)] string function);
    }
}
