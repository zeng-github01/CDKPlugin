using System;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Plugins;

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

[assembly: PluginMetadata("Zengyj.CDKPlugin", DisplayName = "CDKPlugin")]

namespace CDKPlugin
{
    public class MyOpenModPlugin : OpenModUnturnedPlugin
    {
        private readonly IStringLocalizer stringLocalizer;
        private readonly ILogger<MyOpenModPlugin> logger;

        public MyOpenModPlugin(
            IStringLocalizer stringLocalizer,
            ILogger<MyOpenModPlugin> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.stringLocalizer = stringLocalizer;
            this.logger = logger;
        }

        protected override async UniTask OnLoadAsync()
        {
            await UniTask.SwitchToMainThread(); //uncomment if you have to access Unturned or UnityEngine APIs
            logger.LogInformation(stringLocalizer["plugin_events:plugin_start"]);

            //await UniTask.SwitchToThreadPool(); // you can switch back to a different thread
        }

        protected override async UniTask OnUnloadAsync()
        {
            await UniTask.SwitchToMainThread(); //uncomment if you have to access Unturned or UnityEngine APIs
            logger.LogInformation(stringLocalizer["plugin_events:plugin_stop"]);
        }
    }
}