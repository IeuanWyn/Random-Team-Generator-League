using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services;
using System.Net.Http.Headers;
using System.Text;

namespace Commands
{
    public class TeamGeneratorCommands : BaseCommandModule
    {
        readonly HttpHelperService _httpHelperService = new HttpHelperService();

        private ILogger _logger;
        public TeamGeneratorCommands(ILogger logger)
        {
            _logger = logger;
        }

        [Command("gods")]
        public async Task GodsCommand(CommandContext ctx)
        {

            _logger.LogInformation("Gods Command executed.")
            var midChamp = await GetChampion("mid");
            var jungleChamp = await GetChampion("jungle");
            var topChamp = await GetChampion("top");
            var adcChamp = await GetChampion("adc");
            var supportChamp = await GetChampion("support");

            await ctx.RespondAsync($"Top: {(Champions)int.Parse(topChamp)}, Jungle: {(Champions)int.Parse(jungleChamp)}, Mid: {(Champions)int.Parse(midChamp)}, ADC: {(Champions)int.Parse(adcChamp)}, Support: {(Champions)int.Parse(supportChamp)}");
        }

        private async Task<string> GetChampion(string position)
        {
            var response = await _httpHelperService.GetJasonFromAPIAsync(_logger, position);
            return GetRandom(
                    JObject.Parse(
                        await response.Content.ReadAsStringAsync()
                        )
                    );
        }
        private string GetRandom(JObject jasonObject)
        {
            IEnumerable<string>? characterIds =
                from p in jasonObject["data"]
                select (string)p["champion_id"];

            return characterIds.Skip(new Random().Next(0, 40)).FirstOrDefault();
        }
    }
}
