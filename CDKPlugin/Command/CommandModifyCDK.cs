﻿using CDKPlugin.Entities;
using CDKPlugin.Infrastructure.Enum;
using CDKPlugin.Repositories;
using CDKPlugin.Until.Wrapper;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OpenMod.API.Commands;
using OpenMod.API.Persistence;
using OpenMod.API.Users;
using OpenMod.Core.Commands;
using OpenMod.Core.Console;
using OpenMod.Unturned.Commands;
using OpenMod.Unturned.Users;
using SDG.Unturned;
using Serilog;
using SmartFormat.Utilities;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDKPlugin.Command
{
    [Command("modifycdk")]
    [CommandAlias("mcdk")]
    [CommandActor(typeof(UnturnedUser))]
    [CommandActor(typeof(ConsoleActor))]
    [CommandDescription("A command that used to modify cdk in game")]
    [CommandSyntax("<Add|Update|Remove> [Key] [Item/Hand|Vehicle|Reputation|Experience|Money] [ID|Amount] [ItemAmount]")]
    public class CommandModifyCDK : UnturnedCommand
    {
        private readonly IServiceProvider m_ServiceProvider;
        private readonly IStringLocalizer m_StringLocalizer;
        private readonly ICDKPluginRepository m_repository;
        private readonly ILogger<CDKPlugin> m_logger;
        private readonly IUserDataStore m_userDataStore;
        private readonly IDataStore m_DataStore;

        public CommandModifyCDK(IServiceProvider serviceProvider, IStringLocalizer stringLocalizer, ICDKPluginRepository repository, ILogger<CDKPlugin> logger, IUserDataStore userDataStore, IDataStore dataStore):base(serviceProvider)
        {
            m_ServiceProvider = serviceProvider;
            m_StringLocalizer = stringLocalizer;
            m_repository = repository;
            m_logger = logger;
            m_userDataStore = userDataStore;
            m_DataStore = dataStore;
        }

        protected override async UniTask OnExecuteAsync()
        {
            var opra = await Context.Parameters.GetAsync<EKeyOprationType>(0);

            EColunmType? Prisetype = null;
            string? keyCode = null;

            if(Context.Parameters.Length > 1)
            {
                if (!Enum.TryParse<EColunmType>(await Context.Parameters.GetAsync<string>(1), true, out EColunmType result))
                {
                    //已指定密钥
                    keyCode = await Context.Parameters.GetAsync<string>(1);
                    Prisetype = await Context.Parameters.GetAsync<EColunmType>(2);
                }
                else
                {
                    //未指定密钥
                    Prisetype = await Context.Parameters.GetAsync<EColunmType>(1);
                }
            }

            HashSet<string>? modifyingKeys = null;
                var temp = await m_userDataStore.GetUserDataAsync<CDKData>(Context.Actor.Id, Context.Actor.Type, "tempCDK");
                bool v = await m_DataStore.ExistsAsync("modifyingKey");
             if(!v)
             {
                 await m_DataStore.SaveAsync("modifyingKey",new HashSet<string>());
             }
             else
             {
                 modifyingKeys = await m_DataStore.LoadAsync<HashSet<string>>("modifyingKey");
             }

            if (keyCode != null && modifyingKeys != null)
            {
                if (modifyingKeys.Contains(keyCode))
                {
                    throw new UserFriendlyException(m_StringLocalizer["modify_cdk:modifying"]);
                }
                else
                {
                    modifyingKeys.Add(keyCode);
                    await m_DataStore.SaveAsync("modifyingKey", modifyingKeys);
                }
            }

            switch (opra)
            {
                case EKeyOprationType.Add:
                    switch (Prisetype)
                    {
                        case EColunmType.Item:
                            if(Context.Actor is UnturnedUser user)
                            {
                                var itemString = keyCode != null ? await Context.Parameters.GetAsync<string>(3) : await Context.Parameters.GetAsync<string>(2);
                                if (!ushort.TryParse(itemString,out _) && itemString == "hand" && user.Player.Player.equipment.isSelected)
                                {
                                    
                                    var id = user.Player.Player.equipment.itemID;
                                    var amount = user.Player.Player.equipment.asset.amount;
                                    var qua = user.Player.Player.equipment.quality;
                                    var sate = user.Player.Player.equipment.state;
                                    var itemamount = keyCode != null ? await Context.Parameters.GetAsync<byte>(4) : await Context.Parameters.GetAsync<byte>(3);
                                   var itemInfo = CDKItemWrapper.Create(new Item(id, amount, qua, sate), itemamount);

                                    temp ??= keyCode != null ? m_repository.GetCDKData(keyCode) ?? new CDKData() : new CDKData();
                                    temp.Items.Add(itemInfo);
                                    await m_userDataStore.SetUserDataAsync<CDKData>(Context.Actor.Id,
                                        Context.Actor.Type, "tempCDK", temp);
                                    return;
                                }
                            }

                            var itemID = keyCode != null ? await Context.Parameters.GetAsync<ushort>(3) : await Context.Parameters.GetAsync<ushort>(2);
                            var asset = Assets.find(EAssetType.ITEM, itemID);

                            if (asset is ItemAsset itemAsset)
                            {
                                var amount = keyCode != null ? await Context.Parameters.GetAsync<byte>(4) : await Context.Parameters.GetAsync<byte>(3);
                                var itemInfo = CDKItemWrapper.Create(new Item(itemAsset.id, itemAsset.amount, itemAsset.quality, itemAsset.getState()), amount);


                                // 下方代码含义: 当temp等于null时调用??= 为空默认值
                                // 当keyCode不等于null时调用冒号左边单问号右边; true 为冒号左 反为右
                                // 当GetCDKData 为null时调用双问号右边  ?? 为空调用
                                temp ??= keyCode != null ? m_repository.GetCDKData(keyCode) ?? new CDKData() : new CDKData();
                                temp.Items.Add(itemInfo);
                                await m_userDataStore.SetUserDataAsync<CDKData>(Context.Actor.Id,
                                    Context.Actor.Type, "tempCDK", temp);
                            }
                            break;
                        case EColunmType.Vehicle:
                            var vehicleID = keyCode != null ? await Context.Parameters.GetAsync<ushort>(3) : await Context.Parameters.GetAsync<ushort>(2);
                            temp ??= keyCode != null ? m_repository.GetCDKData(keyCode) ?? new CDKData() : new CDKData();
                            temp.Vehicle = vehicleID;
                            await m_userDataStore.SetUserDataAsync<CDKData>(Context.Actor.Id,
                                    Context.Actor.Type, "tempCDK", temp);

                            break;
                        case EColunmType.Reputation:
                            var reputationAmount = keyCode != null ? await Context.Parameters.GetAsync<int>(3) : await Context.Parameters.GetAsync<int>(2);

                            temp ??= keyCode != null ? m_repository.GetCDKData(keyCode) ?? new CDKData() : new CDKData();
                            temp.Reputation = reputationAmount;
                            await m_userDataStore.SetUserDataAsync<CDKData>(Context.Actor.Id,
                                    Context.Actor.Type, "tempCDK", temp);
                            break;
                        case EColunmType.Experience:
                            var expAmount = keyCode != null ? await Context.Parameters.GetAsync<uint>(3) : await Context.Parameters.GetAsync<uint>(2);
                            temp ??= keyCode != null ? m_repository.GetCDKData(keyCode) ?? new CDKData() : new CDKData();
                            temp.Experience = expAmount;
                            await m_userDataStore.SetUserDataAsync<CDKData>(Context.Actor.Id,
                                    Context.Actor.Type, "tempCDK", temp);
                            break;
                        case EColunmType.Money:
                            var moneyAmount = keyCode != null ? await Context.Parameters.GetAsync<decimal>(3) : await Context.Parameters.GetAsync<decimal>(2);
                            temp ??= keyCode != null ? m_repository.GetCDKData(keyCode) ?? new CDKData() : new CDKData();
                            temp.Money = moneyAmount;
                            await m_userDataStore.SetUserDataAsync<CDKData>(Context.Actor.Id,
                                    Context.Actor.Type, "tempCDK", temp);
                            break;
                        case EColunmType.PermissionRole:
                            var permissionString = keyCode != null ? await Context.Parameters.GetAsync<string>(3) : await Context.Parameters.GetAsync<string>(2);
                            temp ??= keyCode != null ? m_repository.GetCDKData(keyCode) ?? new CDKData() : new CDKData();
                            temp.PermissionRoleID = permissionString;
                            await m_userDataStore.SetUserDataAsync<CDKData>(Context.Actor.Id,
                                    Context.Actor.Type, "tempCDK", temp);
                            break;
                        case EColunmType.MaxRedeem:
                            var maxRedeem = keyCode != null ? await Context.Parameters.GetAsync<int>(3) : await Context.Parameters.GetAsync<int>(2);
                            temp ??= keyCode != null ? m_repository.GetCDKData(keyCode) ?? new CDKData() : new CDKData();
                            temp.MaxRedeem = maxRedeem;
                            await m_userDataStore.SetUserDataAsync<CDKData>(Context.Actor.Id,
                                    Context.Actor.Type, "tempCDK", temp);
                            break;
                        default:
                            throw new UserFriendlyException(m_StringLocalizer["error:invaild_priseType"]);
                    }
                    break;
                case EKeyOprationType.Update:
                    if (temp != null)
                    {
                        if (!m_repository.KeyExist(temp.CKey))
                        {
                            m_repository.InsertCDK(temp);
                            await Context.Actor.PrintMessageAsync(m_StringLocalizer["modify_cdk:insert", new { CKey = temp.CKey }]);
                        }
                        else
                        {
                            m_repository.UpdateCDK(temp);
                            await Context.Actor.PrintMessageAsync(m_StringLocalizer["modify_cdk:update"]);
                        }
                        await m_userDataStore.SetUserDataAsync<CDKData>(Context.Actor.Id, Context.Actor.Type, "tempCDK", null);
                        if (keyCode != null && modifyingKeys != null) modifyingKeys.Remove(keyCode);
                        await m_DataStore.SaveAsync("modifyingKey", modifyingKeys);
                    }
                    else
                    {
                        throw new UserFriendlyException(m_StringLocalizer["error:invaild_temp"]);
                    }
                    break;
                case EKeyOprationType.Remove:

                    if (Context.Parameters.Length < 3)
                    {
                        keyCode = keyCode = await Context.Parameters.GetAsync<string>(1);
                        if (string.IsNullOrEmpty(keyCode)) throw new UserFriendlyException(m_StringLocalizer["redeem_error:invaild_keycode"]);
                        m_repository.DeleteCDK(keyCode?.ToUpper() ?? string.Empty);
                        await Context.Actor.PrintMessageAsync(m_StringLocalizer["modify_cdk:remove"]);
                        return;
                    }
                    temp ??= m_repository.GetCDKData(keyCode ?? string.Empty);

                    switch (Prisetype)
                    {
                        case EColunmType.Item:
                            var itemID = keyCode != null ? await Context.Parameters.GetAsync<ushort>(3) : await Context.Parameters.GetAsync<ushort>(2);

                            if (temp != null)
                            {
                                var removedItem = temp.Items.Find(x => x.ItemID == itemID);
                                temp.Items.Remove(removedItem);
                            }
                            else
                            {
                                throw new UserFriendlyException(m_StringLocalizer["error:invaild_temp"]);
                            }

                            break;
                        case EColunmType.Vehicle:
                            //var vehicleID = await Context.Parameters.GetAsync<ushort>(3);
                            if (temp != null)
                            {
                                temp.Vehicle = 0;
                            }
                            else
                            {
                                throw new UserFriendlyException(m_StringLocalizer["error:invaild_temp"]);
                            }
                            break;
                        case EColunmType.PermissionRole:
                            //var permissionString = keyCode != null ? await Context.Parameters.GetAsync<string>(3) : await Context.Parameters.GetAsync<string>(2);
                            if (temp != null)
                            {
                                temp.PermissionRoleID = string.Empty;
                            }
                            else
                            {
                                throw new UserFriendlyException(m_StringLocalizer["error:invaild_temp"]);
                            }
                            break;
                        case EColunmType.Money:
                            if (temp != null)
                            {
                                temp.Money = 0;
                            }
                            else
                            {
                                throw new UserFriendlyException(m_StringLocalizer["error:invaild_temp"]);
                            }
                            break;
                        case EColunmType.Experience:
                            if (temp != null)
                            {
                                temp.Experience = 0;
                            }
                            else
                            {
                                throw new UserFriendlyException(m_StringLocalizer["error:invaild_temp"]);
                            }
                            break;
                        case EColunmType.Reputation:
                            if (temp != null)
                            {
                                temp.Reputation = 0;
                            }
                            else
                            {
                                throw new UserFriendlyException(m_StringLocalizer["error:invaild_temp"]);
                            }
                            break;
                        case EColunmType.MaxRedeem:
                            if (temp != null)
                            {
                                temp.MaxRedeem = 1;
                            }
                            break;
                    }
                    break;
                default:
                    throw new UserFriendlyException(m_StringLocalizer["error:invaild_opration"]);
            }
        }
    }
}
