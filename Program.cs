using DSharpPlus;
using DSharpPlus.CommandsNext;
using Commands;
using StackExchange.Redis;
using Managers;

MainAsync().GetAwaiter().GetResult();

static async Task MainAsync()
{

    ConnectionMultiplexer redisConnection = ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("REDIS_URL"));
    //Step 2: Get the reference of the redis database using the redis connection.
    CacheManager.redisCache = redisConnection.GetDatabase();

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