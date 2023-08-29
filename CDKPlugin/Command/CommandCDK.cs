using System;
using System.Threading.Tasks;
using CDKPlugin.Entities;
using CDKPlugin.Repositories;
using Cysharp.Threading.Tasks;
//using dnlib.DotNet;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
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
            var CDKey = m_repository.GetCDKData(keycode);
            var LogList = m_repository.GetLogData(keycode, Infrastructure.Enum.DbQueryType.ByCDK);
            if(Context.Actor is UnturnedUser player)
            {
                var uplayer = Context.Actor as UnturnedUser;
                if (CDKey != null)
                {
                    if (LogList.Count > 0)
                    {
                        await Context.Actor.PrintMessageAsync(m_StringLocalizer["redeem_cdk:Redeemed"], System.Drawing.Color.Red);
                        return;
                    }


                    CDKey.Items.ForEach(item =>
                    {
                        item.GiveItem(player.Player);
                    });
                    
                    VehicleTool.giveVehicle(player.Player.Player, CDKey.Vehicle);

                    player.Player.Player.skills.askAward(CDKey.Experience);
                    player.Player.Player.skills.askRep(CDKey.Reputation);
                    await m_permissionRoleStore.AddRoleToActorAsync(player, CDKey.PermissionRoleID);
                    await Context.Actor.PrintMessageAsync(m_StringLocalizer["redeeme_cdk:Success"], System.Drawing.Color.Green);
                    var log = new LogData(CDKey.CKey, player.SteamId.m_SteamID, DateTime.Now);
                    m_repository.InsertLog(log);
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