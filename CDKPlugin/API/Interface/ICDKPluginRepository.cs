using System;
using System.Collections.Generic;
using CDKPlugin.Entities;
using MyOpenModPlugin.API.Enum;
using OpenMod.Unturned.Players;

namespace CDKPlugin.API.Interface
{
    public interface ICDKPluginRepository
    {
        public List<LogData> GetData(string parameter, DbQueryType type);

        public List<CDKData> GetCDKData(Guid CKey);

        public CDKRedeemedResult Redeemed(UnturnedPlayer player, Guid Key);

    }
}