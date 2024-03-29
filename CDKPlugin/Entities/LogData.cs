﻿using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDKPlugin.Entities
{
    public class LogData
    {
        
        public int LogID { get; set; }
        public string CDKey { get; set; } = string.Empty;
        public ulong SteamID { get; set; }
        public DateTime RedeemedTime { get; set; }

        public CDKData CDKData { get; set; } = new CDKData();

        public LogData() { }

        public LogData(string CKey, ulong steamID, DateTime redeemedTime)
        {
            CDKey = CKey;
            SteamID = steamID;
            RedeemedTime = redeemedTime;
        }
    }
}
