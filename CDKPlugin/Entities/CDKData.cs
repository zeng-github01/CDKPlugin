using CDKPlugin.Until.Wrapper;
using HarmonyLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenMod.Unturned.Players;
using Org.BouncyCastle.Crypto;
using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace CDKPlugin.Entities
{
    public class CDKData
    {
        //[Key]
        public string CKey { get; set; } = string.Empty;

        public List<CDKItemWrapper> Items { get; set; } = new List<CDKItemWrapper>();

        public ushort Vehicle { get; set; }   

        public int Reputation { get; set; }

        public uint Experience { get; set; }

        public decimal Money { get; set; }


        public string PermissionID { get; set; } = string.Empty;

        public ICollection<LogData> LogDataList { get; set; } = new List<LogData>();
    }
}
