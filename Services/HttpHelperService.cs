using System.Net.Http.Headers;

namespace Services
{
    public class HttpHelperService
    {
        static HttpClient client = new HttpClient();

        public async Task<HttpResponseMessage> GetJasonFromAPIAsync(string position)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://www.op.gg/api/statistics/global/champions/ranked?period=month&tier=gold&position={position}");
                response.EnsureSuccessStatusCode();
                return response;

            } catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
