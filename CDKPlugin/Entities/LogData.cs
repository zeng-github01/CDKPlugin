using System;

namespace CDKPlugin.Entities
{
    public class LogData
    {
        public CDKData CDKData { get; set; } = new CDKData();
        public ulong SteamID { get; set; }
        public DateTime RedeemedTime { get; set; }

        public LogData() { }

        public LogData(CDKData cDKData, ulong steamID, DateTime redeemedTime)
        {
            CDKData = cDKData;
            SteamID = steamID;
            RedeemedTime = redeemedTime;
        }
    }
}
