using DayzlightCommon.Entities;
using DayzlightCommon.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace DayzlightAddon.Providers
{
    public class AddonProvider : IDisposable
    {
        private DbProvider db_ = new DbProvider();
        private Random random_ = new Random();
        private static Dictionary<string, Int64> PlayersCache_ = 
            new Dictionary<string, Int64>();

        public void ServerInit(A2Array a2arr)
        {
            using (var transaction = db_.Database.BeginTransaction())
            {
                try
                {
                    // Clean old records
                    var nowDateTime = DateTime.UtcNow;
                    var delDateTime = DateTime.UtcNow.AddDays(-7);
                    var lastRestartTime = db_.ServerRestartInfo.Where(
                        x => x.TimePoint < delDateTime
                    ).OrderByDescending(
                        x => x.TimePoint
                    ).FirstOrDefault();

                    if (lastRestartTime != null)
                    {
                        db_.ServerRestartInfo.RemoveRange(
                            db_.ServerRestartInfo.Where(x => x.TimePoint < lastRestartTime.TimePoint)
                        );
                        db_.PlayerMovements.RemoveRange(
                            db_.PlayerMovements.Where(x => x.Timepoint.TimePoint < lastRestartTime.TimePoint)
                        );
                        db_.Timepoints.RemoveRange(
                            db_.Timepoints.Where(x => x.TimePoint < lastRestartTime.TimePoint)
                        );
                    }

                    // Update server info
                    db_.ServerRestartInfo.Add(new ServerRestartEntity() {
                        WorldName = a2arr[1],
                        MinCornerX = a2arr[2][0],
                        MinCornerY = a2arr[2][1],
                        MaxCornerX = a2arr[3][0],
                        MaxCornerY = a2arr[3][1],
                        SpawnPointX = a2arr[4][0],
                        SpawnPointY = a2arr[4][1],
                        TimePoint = nowDateTime
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
                        TimePoint = DateTime.UtcNow,
                        ServerFpsMin = a2arr[0],
                        ServerFpsAvg = a2arr[1]
                    });

                    var movementEntities = new List<PlayerMovementEntity>();
                    foreach(var movement in a2arr[2])
                    {
                        string uid = movement[0];
                        string name = movement[1];
                        string compaundCache = uid + "\n" + name;

                        Int64 nameId = -1;
                        if (!PlayersCache_.TryGetValue(compaundCache, out nameId))
                        {
                            var nameInfo = db_.PlayerNames.Include(
                                x => x.PlayerInfo
                            ).FirstOrDefault(
                                x => x.PlayerInfo.Uid == uid && x.Name.Equals(name)
                            );

                            if (nameInfo == null)
                            {
                                var playerInfo = db_.Players.FirstOrDefault(x => x.Uid == uid);
                                if (playerInfo == null)
                                {
                                    playerInfo = db_.Players.Add(new PlayerInfoEntity()
                                    {
                                        Uid = uid,
                                        Color = ColorUtils.DefaultPlayerColors[random_.Next(ColorUtils.DefaultPlayerColors.Length)]
                                    });
                                }

                                nameInfo = db_.PlayerNames.Add(new PlayerNameEntity()
                                {
                                    Name = name,
                                    PlayerInfo = playerInfo
                                });
                                db_.SaveChanges();
                            }

                            nameId = nameInfo.Id;
                            PlayersCache_.Add(compaundCache, nameId);
                        }

                        movementEntities.Add(new PlayerMovementEntity()
                        {
                            PlayerName_Id = nameId,
                            Timepoint = timepoint,
                            PosX = movement[2][0],
                            PosY = movement[2][1],
                            Dir = movement[3],
                            VehicleModel = movement[4], 
                            VehicleType = (VehicleType)(int)movement[5]
                        });
                    }

                    db_.PlayerMovements.AddRange(movementEntities);
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
