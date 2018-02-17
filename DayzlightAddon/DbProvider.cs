using System;
using MySql.Data.MySqlClient;
using System.Text;

namespace DayzlightAddon
{
    public class DbProvider
    {
        MySqlConnection dbConn_ = null;

        public DbProvider(string dbCredentials)
        {
            dbConn_ = new MySqlConnection(dbCredentials);
            dbConn_.Open();
        }

        private string GetVersion()
        {
            var appVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return appVersion.Major + "_" + appVersion.Minor;
        }

        public void ServerInit(string worldName, double[] minCorner, double[] maxCourner, double[] spawnPoint)
        {
            // Create init table
            var cmd = dbConn_.CreateCommand();
            cmd.CommandText =
                "CREATE TABLE IF NOT EXISTS `dayzlight_init_v" + GetVersion() + "` (" +
                "`id`            int UNSIGNED NOT NULL," +
                "`worldname`     varchar(255) NOT NULL," +
                "`mincorner_x`   double NOT NULL," +
                "`mincorner_y`   double NOT NULL," +
                "`maxcorner_x`   double NOT NULL," +
                "`maxcorner_y`   double NOT NULL," +
                "`spawnpos_x`    double NOT NULL," +
                "`spawnpos_y`    double NOT NULL," +
                "`cleanup_plmov` double NOT NULL," + 
                "PRIMARY KEY(`id`)" +
            ");";
            cmd.ExecuteNonQuery();

            // Insert server info into init table
            cmd = dbConn_.CreateCommand();
            cmd.CommandText =
                "INSERT INTO `dayzlight_init_v" + GetVersion() + "` " + 
                    "(`id`, `worldname`, `mincorner_x`, `mincorner_y`, `maxcorner_x`, `maxcorner_y`, `spawnpos_x`, `spawnpos_y`, `cleanup_plmov`) " +
                "VALUES(1, " +
                    "@worldname, @mincorner_x, @mincorner_y, @maxcorner_x, @maxcorner_y, @spawnpos_x, @spawnpos_y, @cleanup_plmov) " +
                "ON DUPLICATE KEY UPDATE " + 
                    "worldname = @worldname, " + 
                    "mincorner_x = @mincorner_x, mincorner_y = @mincorner_y, " +
                    "maxcorner_x = @maxcorner_x, maxcorner_y = @maxcorner_y, " +
                    "spawnpos_x = @spawnpos_x, spawnpos_y = @spawnpos_y;";
            cmd.Parameters.AddWithValue("@worldname", worldName);
            cmd.Parameters.AddWithValue("@mincorner_x", minCorner[0]);
            cmd.Parameters.AddWithValue("@mincorner_y", minCorner[1]);
            cmd.Parameters.AddWithValue("@maxcorner_x", maxCourner[0]);
            cmd.Parameters.AddWithValue("@maxcorner_y", maxCourner[1]);
            cmd.Parameters.AddWithValue("@spawnpos_x", spawnPoint[0]);
            cmd.Parameters.AddWithValue("@spawnpos_y", spawnPoint[1]);
            cmd.Parameters.AddWithValue("@cleanup_plmov", new TimeSpan(1, 0, 0, 0, 0).TotalMilliseconds);
            cmd.ExecuteNonQuery();

            // Create players movment table
            cmd = dbConn_.CreateCommand();
            cmd.CommandText =
                "CREATE TABLE IF NOT EXISTS `dayzlight_plmov_v" + GetVersion() + "` (" +
                "`id`           bigint UNSIGNED NOT NULL AUTO_INCREMENT," +
                "`uid`          bigint UNSIGNED NOT NULL," +
                "`pos_x`        double NOT NULL," +
                "`pos_y`        double NOT NULL," +
                "`dir`          double NOT NULL," +
                "`timepoint`    bigint NOT NULL," +
                "PRIMARY KEY(`id`)," +
                "INDEX `uid_index` (`uid`) USING BTREE," +
                "INDEX `timepoint_index` (`timepoint`) USING BTREE" +
            ");";
            cmd.ExecuteNonQuery();
        }

        public void UpdatePlayersMovement(PlayersMovement[] plmovs)
        {
            if (plmovs.Length == 0)
                return;

            var timepoint = DateTime.UtcNow.ToBinary();
            var builder = new StringBuilder();
            builder.Append("INSERT INTO `dayzlight_plmov_v" + GetVersion() + "` ");
            builder.Append("(`uid`, `pos_x`, `pos_y`, `dir`, `timepoint`) VALUES ");
            foreach(var plmov in plmovs)
            {
                if (!Object.ReferenceEquals(plmovs[0],plmov)) builder.Append(",");
                builder.Append($"({plmov.uid_},{plmov.pos_[0]},{plmov.pos_[1]},{plmov.dir_},{timepoint})");
            }
            builder.Append(";");

            var cmd = dbConn_.CreateCommand();
            cmd.CommandText = builder.ToString();
            cmd.ExecuteNonQuery();
        }
    }
}
