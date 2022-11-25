using System;

namespace CDKPlugin.Entities
{
    public class LogData
    {
        public Guid CKey { get; set; }
        public ulong SteamID { get; set; }
        public DateTime RedeemedTime { get; set; }
    }
}
