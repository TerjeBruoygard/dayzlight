using DayzlightCommon.Entities;
using DayzlightCommon.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace DayzlightAddon.Providers
{
    public class AddonProvider : IDisposable
    {
        private DbProvider db_ = new DbProvider();

        public void ServerInit(A2Array a2arr)
        {
            using (var transaction = db_.Database.BeginTransaction())
            {
                try
                {
                    // Clean old records
                    var delDateTime = DateTime.UtcNow.AddDays(-1);
                    db_.PlayerMovements.RemoveRange(
                        db_.PlayerMovements.Where(x => x.Timepoint.TimePoint < delDateTime)
                    );
                    db_.Timepoints.RemoveRange(
                        db_.Timepoints.Where(x => x.TimePoint < delDateTime)
                    );

                    // Update server info
                    db_.ServerInfo.AddOrUpdate(new ServerInfoEntity() {
                        Id = 1,
                        WorldName = a2arr[1],
                        MinCornerX = a2arr[2][0],
                        MinCornerY = a2arr[2][1],
                        MaxCornerX = a2arr[3][0],
                        MaxCornerY = a2arr[3][1],
                        SpawnPointX = a2arr[4][0],
                        SpawnPointY = a2arr[4][1]
                    });
                    db_.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void UpdatePlayersMovement(A2Array a2arr)
        {
            using (var transaction = db_.Database.BeginTransaction())
            {
                try
                {
                    var timepoint = db_.Timepoints.Add(new TimepointEntity()
                    {
                        TimePoint = DateTime.UtcNow
                    });

                    var movements = new List<PlayerMovementEntity>();
                    foreach(var movement in a2arr[0])
                    {
                        movements.Add(new PlayerMovementEntity()
                        {
                            Uid = Int64.Parse(movement[0]),
                            Timepoint = timepoint,
                            PosX = movement[1][0],
                            PosY = movement[1][1],
                            Dir = movement[2]
                        });
                    }

                    db_.PlayerMovements.AddRange(movements);
                    db_.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Dispose()
        {
            db_.Dispose();
        }
    }
}
