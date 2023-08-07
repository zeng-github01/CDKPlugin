using System;
using System.Collections.Generic;
using System.Linq;
using CDKPlugin.Contexts;
using CDKPlugin.Entities;
//using CDKPlugin.Entities.CDKData;
using CDKPlugin.Infrastructure.Enum;
using OpenMod.Unturned.Players;

namespace CDKPlugin.Repositories
{
    public class CDKPluginRepositoryImplementation : ICDKPluginRepository
    {
        private readonly CDKPluginDbContext m_DbContext;

        public CDKPluginRepositoryImplementation(CDKPluginDbContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public CDKData? GetCDKData(string CKey)
        {
            return m_DbContext.CDKData.Where(x=>x.CKey == CKey).FirstOrDefault();
        }

        public List<LogData> GetLogData(string parameter, DbQueryType type)
        {
            return GetLogDatasInternal(parameter, type).ToList();
        }

        private IQueryable<LogData> GetLogDatasInternal(string parameter, DbQueryType type)
        {
            switch (type)
            {
                case DbQueryType.ByTime:
                    if(DateTime.TryParse(parameter, out var date))
                    {
                        return GetLogDataByTime(date); 
                    }
                    else
                    {
                       return m_DbContext.LogData.Take(0);
                    }
                case DbQueryType.ByCDK:
                    return GetLogDataByCDK(parameter);
                case DbQueryType.BySteamID:
                    return GetLogDataBySteamID(parameter);
                default:
                    return m_DbContext.LogData.Take(0);
            }
        }

        private IQueryable<LogData> GetLogDataByCDK(string searchTerm)
        {
            return m_DbContext.LogData.Where(x => x.Navegation.CKey == searchTerm);
        }

        private IQueryable<LogData> GetLogDataByTime(DateTime searchTerm)
        {
            return m_DbContext.LogData.Where(x => x.RedeemedTime <= searchTerm);
        }

        private IQueryable<LogData> GetLogDataBySteamID(string searchTerm)
        {
            return m_DbContext.LogData.Where(x => x.SteamID.ToString() == searchTerm);
        }

        public void InsertLog(LogData logData)
        {
            m_DbContext.LogData.Add(logData);
        }

        public void UpdateLog(LogData logData)
        {
            m_DbContext.LogData.Update(logData);
        }

        public void DeleteLog(List<LogData> logs)
        {
            m_DbContext.LogData.RemoveRange(logs);
        }

        public void InsertCDK(CDKData cdkData)
        {
            m_DbContext.CDKData.Add(cdkData);
        }

        public void UpdateCDK(CDKData cdkData)
        {
           m_DbContext.CDKData.Update(cdkData);
        }

        public void DeleteCDK(List<CDKData> cDKs)
        {
            m_DbContext.CDKData.RemoveRange(cDKs);
        }
    }
}