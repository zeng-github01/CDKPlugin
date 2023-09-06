using CDKPlugin.Until;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenMod.Core.Commands;
using OpenMod.Unturned.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDKPlugin.Command
{
    [Command("debugcall")]
    public class DebugCommandCall : UnturnedCommand
    {
        private readonly ILogger<CDKPlugin> _logger;
        public DebugCommandCall(IServiceProvider serviceProvider, ILogger<CDKPlugin> logger) : base(serviceProvider)
        {
            _logger = logger;
        }

        protected override async UniTask OnExecuteAsync()
        {
            if(await Context.Parameters.GetAsync<string>(0) == "gcdk")
            {
                _logger.LogInformation($"Generated Key {KeyGenerator.GenerateKey()}");
            }
        }
    }
}
