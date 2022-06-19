using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Enums;
using Newtonsoft.Json.Linq;
using Services;
using System.Linq;

namespace Commands
{
    public class TeamGeneratorCommands : BaseCommandModule
    {
        readonly HttpHelperService _httpHelperService = new HttpHelperService();
        private readonly Dictionary<string, int> _availablePositions = new Dictionary<string, int>()
        {
            {"top",57},
            {"jungle", 46},
            {"mid", 57},
            { "adc", 28},
            {"support",30}
        };
        public TeamGeneratorCommands()
        {
        }

        [Command("gods")]
        public async Task GodsCommand(CommandContext ctx)
        {

            var midChamp = await GetChampion("mid");
            var jungleChamp = await GetChampion("jungle");
            var topChamp = await GetChampion("top");
            var adcChamp = await GetChampion("adc");
            var supportChamp = await GetChampion("support");

            await ctx.RespondAsync($"Top: {(Champions)int.Parse(topChamp)}, Jungle: {(Champions)int.Parse(jungleChamp)}, Mid: {(Champions)int.Parse(midChamp)}, ADC: {(Champions)int.Parse(adcChamp)}, Support: {(Champions)int.Parse(supportChamp)}");
        }

        [Command("gods")]
        public async Task GodsCommand(CommandContext ctx, string position)
        {
            var strPos = position;
            if (position is "bottom" or "bot")
                strPos = "adc";

            if (!_availablePositions.ContainsKey(strPos))
                await ctx.RespondAsync($"Available positons are {string.Join(", ", _availablePositions.Keys.ToList())}");

            var champion = await GetChampion(strPos);

            await ctx.RespondAsync($"For {position} you should take {(Champions)int.Parse(champion)}");
        }

        private async Task<string> GetChampion(string position)
        {
            _availablePositions.TryGetValue(position, out int count);
            return GetRandom(
                    JObject.Parse(
                        await _httpHelperService.GetJasonFromAPIAsync(position)
                        )
                    , count
                    );
        }
        private string GetRandom(JObject jasonObject, int count)
        {
            IEnumerable<string>? characterIds =
                from p in jasonObject["data"]
                select (string)p["champion_id"];

            return characterIds.Skip(new Random().Next(0, count))
                .FirstOrDefault();
        }
    }
}
