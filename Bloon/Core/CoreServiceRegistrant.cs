namespace Bloon.Core
{
    using System;
    using Bloon.Core.Database;
    using Bloon.Core.Discord;
    using Bloon.Core.Services;
    using DSharpPlus;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Serilog.Extensions.Logging;

    public class CoreServiceRegistrant : IServiceRegistrant
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AccountsContext>(options => options.UseMySql(AccountsContext.ConnectionString))
                .AddDbContextPool<BloonContext>(options => options.UseMySql(BloonContext.ConnectionString))
                .AddDbContextPool<AnalyticsContext>(options => options.UseMySql(AnalyticsContext.ConnectionString))
                .AddDbContextPool<IntruderContext>(options => options.UseMySql(IntruderContext.ConnectionString))
                .AddSingleton<ActivityManager>()
                .AddSingleton<BloonLog>()
#pragma warning disable CA2000 // Dispose objects before losing scope
                .AddSingleton(new DiscordClient(new DiscordConfiguration
                {
                    Intents = DiscordIntents.DirectMessages | DiscordIntents.GuildBans | DiscordIntents.GuildMembers
                        | DiscordIntents.GuildMessageReactions | DiscordIntents.GuildMessages | DiscordIntents.GuildPresences
                        | DiscordIntents.Guilds,
                    LoggerFactory = new SerilogLoggerFactory(Log.Logger),
                    MessageCacheSize = 0,
                    Token = Environment.GetEnvironmentVariable("BOT_TOKEN"),
                    TokenType = TokenType.Bot,
                }))
#pragma warning restore CA2000 // Dispose objects before losing scope
                .AddSingleton<FeatureManager>()
                .AddSingleton<JobManager>();
        }
    }
}
