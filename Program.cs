using DSharpPlus;
using DSharpPlus.CommandsNext;
using Commands;
using log4net;
using log4net.Config;
using System.Reflection;

MainAsync().GetAwaiter().GetResult();



static async Task MainAsync()
{
    var discordToken = Environment.GetEnvironmentVariable("DISCORD_BOT_ID");
    if(discordToken == "SET_TOKEN")
    {
        throw new ArgumentNullException(nameof(discordToken));
    }

    var discord = new DiscordClient(new DiscordConfiguration()
    {
        Token = discordToken,
        TokenType = TokenType.Bot,
        Intents = DiscordIntents.AllUnprivileged,
        MinimumLogLevel = LogLevel.Debug
    });

    var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
    {
        StringPrefixes = new[] { "!" }
    });

    commands.RegisterCommands<TeamGeneratorCommands>();
    
    await discord.ConnectAsync();
    await Task.Delay(-1);
}