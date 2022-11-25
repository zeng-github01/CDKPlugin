using System;
using System.Collections.Generic;
using System.Linq;
using CDKPlugin.API.Interface;
using CDKPlugin.Entities;
using MyOpenModPlugin.API.Enum;
using OpenMod.Unturned.Players;

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