using HarmonyLib;
using Microsoft.Extensions.Logging;
using OpenMod.Unturned.Players;
using Org.BouncyCastle.Crypto;
using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace CDKPlugin.Entities
{
    public class CDKData
    {
        public string CKey { get; set; } = string.Empty;

        public List<CDKItemWrapper> Items { get; set; } = new List<CDKItemWrapper>();

        public ushort Vehicle { get; set; }

        public int Reputation { get; set; }

        public uint Experience { get; set; }

        public decimal Money { get; set; }


        //public string PermissionID { get; set; }

        public ICollection<LogData> LogDatas { get; set; } = new List<LogData>();


    }

    public class CDKItemWrapper
    {
        //private readonly ILogger m_logger;

        public CDKItemWrapper(ushort Itemid, byte amount, byte quality, byte[] state, byte count)
        {
            Amount = amount;
            Quality = quality;
            State = state;
            ItemID = Itemid;
            Count = count;
        }

        public ushort ItemID { get; set; }

        public byte Amount { get; set; }

        public byte[] State { get; set; } = Array.Empty<byte>();

        public byte Quality { get; set; }

        public byte Count { get; set; }

        public static CDKItemWrapper Create(Item item, byte count)
        {
            return new CDKItemWrapper(item.id, item.amount, item.quality, item.state, count);
        }

        public virtual Item ToItem() => new(ItemID, Amount, Quality, State);

        public virtual bool GiveItem(UnturnedPlayer player, bool dropIfInventoryFull = true)
        {
            return GiveItemInternal(player, ToItem(), Amount, dropIfInventoryFull);
        }

        private bool GiveItemInternal(UnturnedPlayer player, Item item, ushort amount = 1,
             bool dropIfInventoryIsFull = true)
        {
            try
            {
                var added = false;

                for (var i = 0; i < amount; i++)
                {
                    added = player.Inventory.Inventory.tryAddItem(item, true);
                    if (!added && dropIfInventoryIsFull)
                        ItemManager.dropItem(item, player.Player.transform.position, true, true, true);
                }

                return added;
            }
            catch (Exception e)
            {
                //var caller = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Assembly.GetName().Name;
                //Logger.LogError($"[{caller}] [ERROR] AssetUtil GiveItem: {e.Message}");
                //Logger.LogError($"[{caller}] [ERROR] Details: {e}");

                return false;

                throw e;
            }
        }

        public ItemAsset GetItemAsset()
        {
            return (ItemAsset)Assets.find(EAssetType.ITEM, ItemID);
        }

        public override string ToString()
        {
            var itemName = (Assets.find(EAssetType.ITEM, ItemID) as ItemAsset)?.itemName ?? "Unknown Name";
            return $"ItemID: {ItemID} ({itemName}), Durability: {Quality}, Amount: {Amount}";
        }
    }

    public class VehicleWrapper
    {

    }
}
