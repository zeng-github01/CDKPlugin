using System;
using CDKPlugin.Contexts;
using Cysharp.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Plugins;

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

[assembly: PluginMetadata("Zengyj.CDKPlugin", DisplayName = "CDKPlugin")]

namespace CDKPlugin
{
    public class CDKPlugin : OpenModUnturnedPlugin
    {
        private readonly IStringLocalizer stringLocalizer;
        private readonly ILogger<CDKPlugin> logger;
        private readonly CDKPluginDbContext dbContext;

        public CDKPlugin(IStringLocalizer stringLocalizer, ILogger<CDKPlugin> logger, IServiceProvider serviceProvider, CDKPluginDbContext dbContext) : base(serviceProvider)
        {
            this.stringLocalizer = stringLocalizer;
            this.logger = logger;
            this.dbContext = dbContext;
        }

        protected override async UniTask OnLoadAsync()
        {
            await dbContext.Database.MigrateAsync();
            logger.LogWarning("database migration succeed!");
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