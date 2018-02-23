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
        public DbSet<ServerRestartEntity> ServerRestartInfo { get; set; }
        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<LivemapSettingsEntity> LivemapSettings { get; set; }
        public DbSet<TimepointEntity> Timepoints { get; set; }
        public DbSet<PlayerMovementEntity> PlayerMovements { get; set; }
        public DbSet<PlayerInfoEntity> Players { get; set; }
        public DbSet<PlayerNameEntity> PlayerNames { get; set; }

        public DbProviderBase()
          : base() { }
        
        public DbProviderBase(DbConnection existingConnection)
          : base(existingConnection, false) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ServerRestartEntity>();
            modelBuilder.Entity<AdminEntity>();
            modelBuilder.Entity<LivemapSettingsEntity>();
            modelBuilder.Entity<TimepointEntity>();
            modelBuilder.Entity<PlayerMovementEntity>();
            modelBuilder.Entity<PlayerInfoEntity>();
            modelBuilder.Entity<PlayerNameEntity>();
        }

        public void Open()
        {
            Database.Connection.Open();
        }
    }
}