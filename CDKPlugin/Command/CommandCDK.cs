using System;
using System.Threading.Tasks;
using CDKPlugin.Entities;
using CDKPlugin.Repositories;
using Cysharp.Threading.Tasks;
//using dnlib.DotNet;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OpenMod.API.Commands;
using OpenMod.API.Localization;
using OpenMod.API.Permissions;
using OpenMod.API.Users;
using OpenMod.Core.Commands;
using OpenMod.Core.Permissions;
using OpenMod.Core.Users;
using OpenMod.Unturned.Commands;
using OpenMod.Unturned.Users;
using SDG.Unturned;
using SmartFormat.Utilities;

namespace CDKPlugin.Command
{
    [Command("cdk")]
    [CommandActor(typeof(UnturnedUser))]
    public class CommandCDK : UnturnedCommand
    {
        private readonly IServiceProvider m_ServiceProvider;
        private readonly IStringLocalizer m_StringLocalizer;
        private readonly ICDKPluginRepository m_repository;
        private readonly ILogger<CDKPlugin> m_logger;
        private readonly IPermissionRoleStore m_permissionRoleStore;
        public CommandCDK(IServiceProvider serviceProvider, IStringLocalizer mStringLocalizer, ICDKPluginRepository repository, ILogger<CDKPlugin> logger, IPermissionRoleStore permissionRoleStore) : base(serviceProvider)
        {
            m_ServiceProvider = serviceProvider;
            m_StringLocalizer = mStringLocalizer;
            m_repository = repository;
            m_logger = logger;
            m_permissionRoleStore = permissionRoleStore;
        }


        protected override async UniTask OnExecuteAsync()
        {
            var keycode = await Context.Parameters.GetAsync<string>(0);
            var CDKey = m_repository.GetCDKData(keycode.ToUpper());
            var LogList = m_repository.GetLogData(Context.Actor.Id, Infrastructure.Enum.DbQueryType.BySteamID);
            if(Context.Actor is UnturnedUser player)
            {
                if (CDKey != null)
                {
                    if (LogList.Find(x => x.CDKey == CDKey.CKey) != null)
                    {
                        await Context.Actor.PrintMessageAsync(m_StringLocalizer["redeem_cdk:Redeemed"], System.Drawing.Color.Yellow);
                        return;
                    }

                    if(m_repository.GetKeyRedeemCount(CDKey.CKey) >= CDKey.MaxRedeem)
                    {
                        await Context.Actor.PrintMessageAsync(m_StringLocalizer["redeem:MaxRedeem"],System.Drawing.Color.Yellow);
                        return;
                    }

                    await UniTask.SwitchToMainThread();


                    foreach (var item in CDKey.Items)
                    {
                        var res = item.TryGiveItem(player.Player);
                        if (!res)
                        {
                            m_repository.UpdateCDK(CDKey);
                            throw new UserFriendlyException(m_StringLocalizer["error:giveItem"]);
                        }
                        else
                        {
                            CDKey.Items.Remove(item);
                        }
                    }
                    if(CDKey.Vehicle != 0)
                    {
                        VehicleTool.giveVehicle(player.Player.Player, CDKey.Vehicle);
                    }
                    
                    if(CDKey.Experience != 0)
                    {
                        player.Player.Player.skills.askAward(CDKey.Experience);
                    }

                    if(CDKey.Reputation != 0)
                    {
                        player.Player.Player.skills.askRep(CDKey.Reputation);
                    }

                    if(!string.IsNullOrEmpty(CDKey.PermissionRoleID))
                    {
                        await m_permissionRoleStore.AddRoleToActorAsync(player, CDKey.PermissionRoleID);
                    }
                    
                    
                    
                    await Context.Actor.PrintMessageAsync(m_StringLocalizer["redeem_cdk:Success"], System.Drawing.Color.Green);
                    var log = new LogData(CDKey.CKey, player.SteamId.m_SteamID, DateTime.Now);
                    m_repository.InsertLog(log);
                    await UniTask.SwitchToThreadPool();
                }
                else
                {
                    await Context.Actor.PrintMessageAsync(m_StringLocalizer["redeem_cdk:KeyNotFound"], System.Drawing.Color.Red);
                    return;
                }
            }
            
        }
    }
}