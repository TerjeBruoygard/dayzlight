using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DayzlightAddonTests
{
    class EntryPoint
    {
        static readonly string DbRedentials = File.ReadAllText(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dayzlight", "DB.ini")
        );

        static void Main(string[] args)
        {
            try
            {
                // Initialize server
                Console.Write("Initialize server...");
                AddonWrapper.RVExtensionManaged(
                    $"[\"INIT\",[\"{DbRedentials}\",\"namalsk\",[0,0],[12800,12800],[78,112.5]]]"
                );
                Console.WriteLine("OK");

                // Write movement data
                int itersCount = 100;
                var playerCount = 15;
                var players = new double[playerCount, 2];
                var rand = new Random();
                for (int i = 1; i <= itersCount; i++)
                {
                    Console.Write($"Write movement data {i} of {itersCount}...");
                    var movs = "[";
                    for (int p = 0; p < playerCount; p++)
                    {
                        for (int d = 0; d < 2; d++)
                        {
                            if (players[p, d] == 0d) players[p, d] = (rand.NextDouble() * 5000) + 2500;
                            players[p, d] += (rand.NextDouble() * 100) - 50;
                        }
                        if (p != 0) movs += ",";
                        long uid = 76561198000000000;
                        double cord = (p * 100d) + (i * 100d);
                        movs += $"[\"{uid + p}\", \"Player_{p}\", [{players[p,0]},{players[p,1]}],{rand.NextDouble() * 360d}]";
                    }
                    movs += "]";
                    AddonWrapper.RVExtensionManaged(
                        $"[\"STAT\",[{movs}]]"
                    );
                    Console.WriteLine("OK");
                    Thread.Sleep(1000);
                }

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
