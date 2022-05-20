using DSharpPlus;
using DSharpPlus.CommandsNext;
using Commands;
using Services;

MainAsync().GetAwaiter().GetResult();

static async Task MainAsync()
{
    var discord = new DiscordClient(new DiscordConfiguration()
    {
        Token = GoogleCloudService.GetDiscordSecret(),
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