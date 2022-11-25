using System.Collections.Generic;
using CDKPlugin.Entities;
using MyOpenModPlugin.API.Enum;
using OpenMod.Unturned.Players;

namespace CDKPlugin.API.Interface
{
    public interface ICDKPluginRepository
    {
        //public IQueryable<CDKData> GetCdkData(string cKey);

        //public IQueryable<LogData> GetLogData(string parameter,DbQueryType type);

        public List<LogData> GetData(string parameter, DbQueryType type);

        public List<CDKData> GetCDKData(string CKey);

        public CDKRedeemedResult Redeemed(UnturnedPlayer player, string Key);

    }
}