using System;
using System.Collections.Generic;
using CDKPlugin.Entities;
using CDKPlugin.Infrastructure.Enum;
using OpenMod.Unturned.Players;

namespace CDKPlugin.Repositories
{
    public interface ICDKPluginRepository
    {
        public List<LogData> GetLogData(string parameter, DbQueryType type);

        public CDKData GetCDKData(string Ckey);

        public void InsertLog(LogData logData);

        public void UpdateLog(LogData logData);

        public void DeleteLog(List<LogData> logs);

        public void InsertCDK(CDKData cdkData);

        public void UpdateCDK(CDKData cdkData);

        public void DeleteCDK(List<CDKData> cDKs); 

    }
}