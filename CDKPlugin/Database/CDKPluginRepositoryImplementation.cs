using System;
using System.Collections.Generic;
using CDKPlugin.API.Interface;
using CDKPlugin.Contexts;
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

        public List<CDKData> GetCDKData(Guid CKey)
        {
            throw new NotImplementedException();
        }

        public List<LogData> GetData(string parameter, DbQueryType type)
        {
            throw new NotImplementedException();
        }

        public CDKRedeemedResult Redeemed(UnturnedPlayer player, Guid Key)
        {
            throw new NotImplementedException();
        }
    }
}