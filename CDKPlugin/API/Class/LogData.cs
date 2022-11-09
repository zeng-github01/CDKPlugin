using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CDKPlugin.API.Class
{
    public class LogData
    {
        //private DateTime Now = DateTime.Now;
        [Required]
        public string CKey { get; set; } = String.Empty;

        [Required]
        public ulong SteamID { get; set; }

        [Required]
        //[DefaultValue()]
        public DateTime RedeemedTime { get; set; }


    }
}