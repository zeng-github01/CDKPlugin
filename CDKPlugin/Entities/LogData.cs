using System;
using System.ComponentModel.DataAnnotations;

namespace CDKPlugin.Entities
{
    public class LogData
    {
        [Required]
        public string? CKey { get; set; }

        [Required]
        public ulong SteamID { get; set; }

        [Required]
        public DateTime RedeemedTime { get; set; }


    }
}
