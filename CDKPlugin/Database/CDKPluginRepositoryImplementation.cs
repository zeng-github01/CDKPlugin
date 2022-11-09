using System;
using System.Collections.Generic;
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
        private IQueryable<CDKData> GetCdkDataInternal(string ckey)
        {
            // throw new NotImplementedException();
            return m_DbContext.CdkData.Where(x => x.CKey == ckey);
        }

        private IQueryable<LogData> GetLogDataInternal(string parameter, DbQueryType type)
        {
            // throw new System.NotImplementedException();
            switch (type)
            {
                case DbQueryType.ByCDK:
                    return m_DbContext.LogData.Where(x => x.CKey == parameter);
                case DbQueryType.BySteamID:
                    if (ulong.TryParse(parameter, out ulong steamid))
                    {
                        return m_DbContext.LogData.Where(x => x.SteamID == steamid);
                    }
                    return m_DbContext.LogData.Take(0);
                case DbQueryType.ByTime:
                    return m_DbContext.LogData.Where(x=> x)

            }
        }

       

        public CDKRedeemedResult Redeemed(UnturnedPlayer player, string Key)
        {
            throw new System.NotImplementedException();
        }

        public List<LogData> GetData(string parameter, DbQueryType type)
        {
            throw new NotImplementedException();
        }

        public List<CDKData> GetCDKData(string CKey)
        {
            throw new NotImplementedException();
        }
    }
}