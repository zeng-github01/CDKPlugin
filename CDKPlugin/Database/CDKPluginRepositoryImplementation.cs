using System;
using System.Linq;
using CDKPlugin.API.Class;
using CDKPlugin.API.Interface;
using MyOpenModPlugin.API.Enum;
using MySql.Data.MySqlClient;
using OpenMod.Unturned.Players;
using Steamworks;

namespace CDKPlugin.Database
{
    public class CDKPluginRepositoryImplementation : ICDKPluginRepository
    {
        private readonly CDKPluginDbContext m_DbContext;

        public CDKPluginRepositoryImplementation(CDKPluginDbContext managerDbContext)
        {
            m_DbContext = managerDbContext;
        }
        public IQueryable<CDKData> GetCdkData(string ckey)
        {
            // throw new NotImplementedException();
            return m_DbContext.CdkData.Where(x => x.CKey == ckey);
        }

        public IQueryable<LogData> GetLogData(string parameter, DbQueryType type)
        {
            // throw new System.NotImplementedException();
            switch (type)
            {
                case DbQueryType.ByCDK:
                    return m_DbContext.LogData.Where(x => x.CKey == parameter);
                break;
                case DbQueryType.BySteamID:
                    if (ulong.TryParse(parameter, out ulong steamid))
                    {
                        return m_DbContext.LogData.Where(x => x.SteamID == steamid);
                    }
                    
                    break;

            }
        }

        public CDKRedeemedResult Redeemed(UnturnedPlayer player, string Key)
        {
            throw new System.NotImplementedException();
        }

        // private CDKData buildCdkData(MySqlDataReader reader)
        // {
        //     return new CDKData();
        // }
        //
        // private LogData Build
    }
}