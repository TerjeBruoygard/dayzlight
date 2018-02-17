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
            var output = new StringBuilder();
            var outputSize = 1024;
            var function = $"[INIT,[{DbRedentials},namalsk,[0,12800],[0,12800],[78,112.5]]]";
            AddonWrapper.RVExtension(output, outputSize, function);
            Console.WriteLine(output.ToString());
        }
    }
}
