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
        public DbSet<TimepointEntity> Timepoints { get; set; }
        public DbSet<PlayerMovementEntity> PlayerMovements { get; set; }

        public DbProviderBase()
          : base() { }
        
        public DbProviderBase(DbConnection existingConnection)
          : base(existingConnection, false) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ServerInfoEntity>();
            modelBuilder.Entity<AdminEntity>();
            modelBuilder.Entity<TimepointEntity>();
            modelBuilder.Entity<PlayerMovementEntity>();
        }

        public void Open()
        {
            Database.Connection.Open();
        }
    }
}