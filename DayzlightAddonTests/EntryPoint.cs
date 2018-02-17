using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayzlightAddonTests
{
    class EntryPoint
    {
        static readonly string DbRedentials = "Persist Security Info=False;server=localhost;database=namalsk_reloaded;uid=mysql;password=mysql";

        static void Main(string[] args)
        {
            try
            {
                Console.Write("Initialize server...");
                AddonWrapper.RVExtensionManaged(
                    $"[\"INIT\",[\"{DbRedentials}\",\"namalsk\",[0,12800],[0,12800],[78,112.5]]]"
                );
                Console.WriteLine("OK");

                // Write test data
                // TODO

                Console.WriteLine("All tests compleated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:");
                Console.WriteLine(ex);
            }
        }
    }
}
