using System;
using System.Collections.Generic;
using CDKPlugin.Entities;
using CDKPlugin.Infrastructure.Enum;
using OpenMod.Unturned.Players;

namespace CDKPlugin.Repositories
{
    public interface ICDKPluginRepository
    {
        public List<LogData> GetData(string parameter, DbQueryType type);

        public List<CDKData> GetCDKData(Guid CKey);

        public CDKRedeemedResult Redeemed(UnturnedPlayer player, Guid Key);

    }
}