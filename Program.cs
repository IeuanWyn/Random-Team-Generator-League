using DSharpPlus;
using DSharpPlus.CommandsNext;
using Commands;
using log4net;
using log4net.Config;
using System.Reflection;

var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

MainAsync().GetAwaiter().GetResult();



static async Task MainAsync()
{
    ILog logger = LogManager.GetLogger(typeof(Program));
    var discordToken = Environment.GetEnvironmentVariable("DISCORD_BOT_ID");
    if(discordToken == "SET_TOKEN")
    {
        logger.Error("Unable to find Environment Variable DISCORD_BOT_ID");
        throw new ArgumentNullException(nameof(discordToken));
    }

    logger.Info("Attempting to connect to Discord Client");
    var discord = new DiscordClient(new DiscordConfiguration()
    {
        Token = discordToken,
        TokenType = TokenType.Bot,
        Intents = DiscordIntents.AllUnprivileged,
        MinimumLogLevel = LogLevel.Debug
    });
    logger.Info("Connected to Discord Client");
    var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
    {
        StringPrefixes = new[] { "!" }
    });

    commands.RegisterCommands<TeamGeneratorCommands>();
    
    await discord.ConnectAsync();
    logger.Info("Completed");
    await Task.Delay(-1);
}