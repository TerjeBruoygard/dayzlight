using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;

namespace DayzlightWebapp.Providers
{
    public class DbProvider
    {
        private static readonly string dbCredentials_ = File.ReadAllText(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB.ini")
        );

        public MySqlConnection CreateConnection()
        {
            var dbConn_ = new MySqlConnection(dbCredentials_);
            dbConn_.Open();
            return dbConn_;
        }
    }
}