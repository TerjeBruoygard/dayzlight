using System;
using System.IO;
using System.Data.Common;
using System.Data.Entity;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using DayzlightCommon.Entities;

namespace DayzlightCommon.Providers
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DbProviderBase : DbContext
    {
        public DbSet<ServerInfoEntity> ServerInfo { get; set; }
        public DbSet<AdminEntity> Admins { get; set; }

        public DbProviderBase()
          : base() { }
        
        public DbProviderBase(DbConnection existingConnection)
          : base(existingConnection, false) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ServerInfoEntity>();
            modelBuilder.Entity<AdminEntity>();
        }

        public void Open()
        {
            Database.Connection.Open();
        }
    }
}