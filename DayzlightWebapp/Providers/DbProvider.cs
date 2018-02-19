using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data.Entity;
using System.Data.Common;
using System.Data.Entity;
using DayzlightCommon.Providers;

namespace DayzlightWebapp.Providers
{
    public class DbProvider : DbProviderBase
    {
        private static readonly string dbCredentials_ = File.ReadAllText(
            Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/bin"), "DB.ini")
        );
        
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