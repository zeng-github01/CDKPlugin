using CDKPlugin.Entities;
using CDKPlugin.Repositories;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OpenMod.API.Users;
using OpenMod.Core.Commands;
using OpenMod.Core.Console;
using OpenMod.Unturned.Commands;
using OpenMod.Unturned.Users;
using SDG.Unturned;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDKPlugin.Command
{
    [Command("createcdk")]
    [CommandActor(typeof(UnturnedUser))]
    [CommandActor(typeof(ConsoleActor))]
    [CommandDescription("A command that used to create or modify cdk in game")]
    [CommandSyntax(" <Add|Update|Remove> <Key> [Item/Hand|Vehicle|Reputation|Experience|Money]  [ID|Amount] [Amount]")]
    public class CommandCreateCDK : UnturnedCommand
    {
        private readonly IServiceProvider m_ServiceProvider;
        private readonly IStringLocalizer m_StringLocalizer;
        private readonly ICDKPluginRepository m_repository;
        //private readonly IUserManager m_userManager;
        private readonly ILogger<CDKPlugin> m_logger;
        private readonly IUserDataStore m_userDataStore;
        public CommandCreateCDK(IServiceProvider serviceProvider, ICDKPluginRepository repository, IStringLocalizer stringLocalizer, ILogger<CDKPlugin> logger,IUserDataStore userData) : base(serviceProvider)
        {
            m_ServiceProvider = serviceProvider;
            m_repository = repository;
            m_StringLocalizer = stringLocalizer;
            m_logger = logger;
            m_userDataStore = userData;
        }

        protected override async UniTask OnExecuteAsync()
        {
            var opra = await Context.Parameters.GetAsync<string>(0);
            var keycode = await Context.Parameters.GetAsync<string>(1);
            var type = await Context.Parameters.GetAsync<string>(2);
            var userData = await m_userDataStore.GetUserDataAsync(Context.Actor.Id, Context.Actor.Type);

            switch (opra)
            {
                case "add":
                    switch (type)
                    {
                        case "item":
                            var itemID = await Context.Parameters.GetAsync<ushort>(3);
                            var asset = Assets.find(EAssetType.ITEM, itemID);

                            if (asset is ItemAsset itemAsset)
                            {
                                var amount = await Context.Parameters.GetAsync<byte>(2);
                                var itemInfo = CDKItemWrapper.Create(new Item(itemAsset.id, itemAsset.amount, itemAsset.quality, itemAsset.getState()), amount);
#pragma warning disable CS8602 // 解引用可能出现空引用。
                                if (userData.Data.ContainsKey("cdkTemp"))
                                {
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                                    var tempData = (CDKData)userData.Data["cdkTemp"];
                                    tempData.Items.Add(itemInfo);
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                                }
                                else
                                {
                                    var cdkData = new CDKData();
                                    cdkData.CKey = keycode;
                                    cdkData.Items.Add(itemInfo);
                                    userData?.Data?.Add("cdkTemp", cdkData);
                                    userData.Data.Add("cdkTemp", cdkData);
                                }
#pragma warning restore CS8602 // 解引用可能出现空引用。
                                


                            }

                            break;

                    }
                    break;
                case "modify":
                    break;
                case "remove":
                    break;

            }

        }

        private async CDKData GetTempData()
        {
            CDKData? data = null;
            var userData = await m_userDataStore.GetUserDataAsync(Context.Actor.Id, Context.Actor.Type);
#pragma warning disable CS8602 // 解引用可能出现空引用。
            if (userData.Data.TryGetValue("cdkTemp",out object? parse))
            {
                data = parse as CDKData;
            }
#pragma warning restore CS8602 // 解引用可能出现空引用。

             return Task.FromResult(data);
        }
    }
}
