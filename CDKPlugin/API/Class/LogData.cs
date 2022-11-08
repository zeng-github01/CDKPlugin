using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CDKPlugin.API.Class
{
    public class LogData
    {
        public string CKey { get; set; } = String.Empty;
        public ulong SteamID { get; set; }
    }
}