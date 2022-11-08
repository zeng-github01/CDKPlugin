using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OpenMod.API.Localization;
using OpenMod.Core.Commands;
using OpenMod.Unturned.Commands;
using OpenMod.Unturned.Users;

namespace MyOpenModPlugin.Command
{
    [Command("cdk")]
    [CommandActor(typeof(UnturnedUser))]
    public class CommandCDK : UnturnedCommand
    {
        private readonly IServiceProvider m_ServiceProvider;
        // private readonly IOpenModStringLocalizer
        private readonly IStringLocalizer m_StringLocalizer;
        public CommandCDK(IServiceProvider serviceProvider, IStringLocalizer mStringLocalizer) : base(serviceProvider)
        {
            m_ServiceProvider = serviceProvider;
            m_StringLocalizer = mStringLocalizer;
        }


        protected override UniTask OnExecuteAsync()
        {
            
            throw new NotImplementedException();
        }
    }
}