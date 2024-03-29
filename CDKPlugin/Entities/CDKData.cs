﻿using CDKPlugin.Until;
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
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace CDKPlugin.Entities
{
    public class CDKData
    {
        public string CKey { get; set; } = KeyGenerator.GenerateKey();

        public List<CDKItemWrapper> Items { get; set; } = new List<CDKItemWrapper>();

        public ushort Vehicle { get; set; }   

        public int Reputation { get; set; }

        public uint Experience { get; set; }

        public decimal Money { get; set; }

        public int MaxRedeem { get; set; } = 1;

        public string PermissionRoleID { get; set; } = string.Empty;

        public ICollection<LogData> LogDataList { get; set; } = new List<LogData>();
    }
}
