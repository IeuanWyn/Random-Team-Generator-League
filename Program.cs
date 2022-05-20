using DSharpPlus;
using DSharpPlus.CommandsNext;
using Commands;
using Microsoft.Extensions.Logging;

MainAsync().GetAwaiter().GetResult();



static async Task MainAsync()
{
    var logger = CreateLogger();
    var discordToken = Environment.GetEnvironmentVariable("DISCORD_BOT_ID");
    if(discordToken == "SET_TOKEN")
    {
        logger.LogError("Unable to find Environment Variable DISCORD_BOT_ID");
        throw new ArgumentNullException(nameof(discordToken));
    }

    logger.LogInformation("Attempting to connect to Discord Client");
    var discord = new DiscordClient(new DiscordConfiguration()
    {
        Token = discordToken,
        TokenType = TokenType.Bot,
        Intents = DiscordIntents.AllUnprivileged,
        MinimumLogLevel = LogLevel.Debug
    });
    logger.LogInformation("Connected to Discord Client");
    var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
    {
        StringPrefixes = new[] { "!" }
    });

    commands.RegisterCommands<TeamGeneratorCommands>();
    
    await discord.ConnectAsync();
    logger.LogInformation("Completed");
    await Task.Delay(-1);
}

static ILogger CreateLogger()
{
    var loggerFactory = LoggerFactory.Create(builder => builder
    .AddFilter("DiscordLogger", LogLevel.Trace)
    .AddSimpleConsole(opts =>
    {
        opts.SingleLine = true;
        opts.IncludeScopes = true;
        opts.TimestampFormat = "dd/MM/yyyy HH:mm:ss.fff ";
    }));
    return loggerFactory.CreateLogger("DiscordLogger");
}