using System;
using MySql.Data.MySqlClient;
using System.Text;
using DayzlightCommon.Providers;

namespace DayzlightAddon.Providers
{
    public class DbProvider : DbProviderBase
    {
        private static string dbCredentials_ = null;

        public static void Initialize(string dbCredentials)
        {
            dbCredentials_ = dbCredentials;
            using (var contextDB = new DbProvider(false))
            {
                contextDB.Database.CreateIfNotExists();
                contextDB.Open();
            }
        }

        public DbProvider(bool autoOpen = true)
            : base(new MySqlConnection(dbCredentials_))
        {
            if (autoOpen)
            {
                Database.Connection.Open();
            }
        }
    }
}
