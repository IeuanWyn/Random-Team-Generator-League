using System.Net.Http.Headers;

namespace Services
{
    public class HttpHelperService
    {
        static HttpClient client = new HttpClient();

        public async Task<HttpResponseMessage> GetJasonFromAPIAsync(ILogger log, string position)
        {

            //await ctx.RespondAsync("Greetings! Thank you for executing me!");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("Referer", "https://www.op.gg/statistics/champions?tier=gold&region=global&position=");
            log.LogInformation("Attempting to retrieve API Information"); ;
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://www.op.gg/api/statistics/global/champions/ranked?period=month&tier=gold&position={position}");
                response.EnsureSuccessStatusCode();
                return response;

            } catch (Exception ex)
            {
                log.LogInformation(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
