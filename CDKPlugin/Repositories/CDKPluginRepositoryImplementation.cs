using System;
using System.Collections.Generic;
using System.Linq;
using CDKPlugin.Contexts;
using CDKPlugin.Entities;
using CDKPlugin.Infrastructure.Enum;
using OpenMod.API.Ioc;
using OpenMod.Unturned.Players;

namespace CDKPlugin.Repositories
{
    [ServiceImplementation]
    public class CDKPluginRepositoryImplementation : ICDKPluginRepository
    {
        private readonly CDKPluginDbContext m_DbContext;

        public CDKPluginRepositoryImplementation(CDKPluginDbContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public CDKData? GetCDKData(string CKey)
        {
            if (string.IsNullOrEmpty(CKey)) return null;

            return m_DbContext.CDKData.Where(x => x.CKey == CKey).FirstOrDefault();
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
            m_DbContext.SaveChanges();
        }

        public void UpdateLog(LogData logData)
        {
            m_DbContext.LogData.Update(logData);
            m_DbContext.SaveChanges();
        }

        public void DeleteLog(List<LogData> logs)
        {
            m_DbContext.LogData.RemoveRange(logs);
            m_DbContext.SaveChanges();
        }

        public void InsertCDK(CDKData cdkData)
        {
            m_DbContext.CDKData.Add(cdkData);
            m_DbContext.SaveChanges();
        }

        public void UpdateCDK(CDKData cdkData)
        {
           m_DbContext.CDKData.Update(cdkData);
            m_DbContext.SaveChanges();
        }

        public void DeleteCDKS(List<CDKData> cDKs)
        {
            m_DbContext.CDKData.RemoveRange(cDKs);
            m_DbContext.SaveChanges();
        }

        public bool KeyExist(string Ckey)
        {
            return m_DbContext.CDKData.Any(x=> x.CKey.Equals(Ckey));
        }

        public void DeleteCDK(string Ckey)
        {
            var beRemove = GetCDKData(Ckey);
            if (beRemove == null) return; 
            m_DbContext.CDKData.Remove(beRemove);
            m_DbContext.SaveChanges();
        }
    }
}