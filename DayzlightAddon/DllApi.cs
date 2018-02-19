using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using RGiesecke.DllExport;
using DayzlightCommon.Utils;
using DayzlightAddon.Providers;

namespace DayzlightAddon
{
    public class DllApi
    {
        [DllExport("_RVExtension@12", CallingConvention = CallingConvention.Winapi)]
        public static void RVExtension(StringBuilder output, int outputSize, [MarshalAs(UnmanagedType.LPStr)] string function)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            string result = string.Empty;
            outputSize--;
            try
            {
                RVExtensionManaged(function);
                result = "OK";
            }
            catch(Exception ex)
            {
                result = $"ERROR: {ex.Message}\n{ex.StackTrace}";
            }

            if (result.Length > outputSize)
            {
                result = result.Substring(0, outputSize);
            }
            output.Append(result);
        }

        public static void RVExtensionManaged(string data)
        {
            A2Array request = A2Array.Deserialize(data);
            String fncName = request[0];
            A2Array fncArgs = request[1];
            switch (fncName)
            {
                case "INIT":
                    DbProvider.Initialize(fncArgs[0]);
                    using (var addonProvider = new AddonProvider())
                    {
                        addonProvider.ServerInit(fncArgs);
                    }
                    break;

                case "STAT":
                    using (var addonProvider = new AddonProvider())
                    {
                        addonProvider.UpdatePlayersMovement(fncArgs);
                    }
                    break;

                default:
                    throw new AccessViolationException($"Extension function '{fncName}' does not supported!");
            }
        }
    }
}
