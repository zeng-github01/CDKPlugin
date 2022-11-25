using System;
using System.Collections.Generic;
using CDKPlugin.Contexts;
using CDKPlugin.Entities;
using CDKPlugin.Infrastructure.Enum;
using OpenMod.Unturned.Players;

namespace CDKPlugin.Repositories
{
    public class CDKPluginRepository : ICDKPluginRepository
    {
        private readonly CDKPluginDbContext m_DbContext;

        public CDKPluginRepository(CDKPluginDbContext managerDbContext)
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