using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenMod.Core.Ioc;
using OpenMod.Unturned.Players;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDKPlugin.Until.Wrapper
{
    [DontAutoRegister]
    [Owned]
    public class CDKItemWrapper
    {
        public CDKItemWrapper(ushort Itemid, byte amount, byte quality, byte[] state, byte count)
        {
            Amount = amount;
            Quality = quality;
            State = state;
            ItemID = Itemid;
            Count = count;
        }

        public CDKItemWrapper() { }

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

        public virtual bool TryGiveItem(UnturnedPlayer player, bool dropIfInventoryFull = true)
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
            catch
            {
                //var caller = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Assembly.GetName().Name;
                //Logger.LogError($"[{caller}] [ERROR] AssetUtil GiveItem: {e.Message}");
                //Logger.LogError($"[{caller}] [ERROR] Details: {e}");
                return false;
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
}
