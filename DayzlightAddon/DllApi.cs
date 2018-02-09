using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

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

        public static string DbCredentials = null;

        public static void RVExtensionManaged(string data)
        {
            A2Array request = A2Array.Deserialize(data);
            String fncName = request[0];
            A2Array fncArgs = request[1];
            switch (fncName)
            {
                case "INIT":
                    DbCredentials = fncArgs[0];
                    new DbProvider(DbCredentials).ServerInit(fncArgs[1], fncArgs[2], fncArgs[3], fncArgs[4]);
                    break;

                case "STAT":
                    var fncArgsPlmovs = fncArgs[0];
                    var plmov = new PlayersMovement[fncArgsPlmovs.Length()];
                    for(int i = 0; i < plmov.Length; ++i)
                    {
                        var fncArgsPlmov = fncArgsPlmovs[i];
                        plmov[i] = new PlayersMovement()
                        {
                            uid_ = UInt64.Parse(fncArgsPlmov[0]),
                            pos_ = fncArgsPlmov[1],
                            dir_ = fncArgsPlmov[2]
                        };
                    }                  
                    new DbProvider(DbCredentials).UpdatePlayersMovement(plmov);
                    break;

                default:
                    throw new AccessViolationException($"Extension function '{fncName}' does not supported!");
            }
        }
    }
}
