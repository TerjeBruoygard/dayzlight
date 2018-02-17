using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DayzlightAddonTests
{
    public class AddonWrapper
    {
        [DllImport("DayzlightAddon.dll", EntryPoint = "_RVExtension@12", CallingConvention = CallingConvention.Winapi)]
        public static extern void RVExtension(StringBuilder output, int outputSize, [MarshalAs(UnmanagedType.LPStr)] string function);

        public static void RVExtensionManaged(string function)
        {
            var outputSize = 1024;
            var output = new StringBuilder(outputSize);
            AddonWrapper.RVExtension(output, outputSize, function);
            if (!output.ToString().Equals("OK"))
            {
                throw new Exception(output.ToString()); 
            }
        }
    }
}
