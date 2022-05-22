using Managers;
using System.Net.Http.Headers;

namespace Services
{
    public class HttpHelperService
    {
        static HttpClient client = new HttpClient();

        public async Task<string> GetJasonFromAPIAsync(string position)
        {
            var returnString = CacheManager.Get(position);
            if (returnString != null)
                return returnString;

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://www.op.gg/api/statistics/global/champions/ranked?period=month&tier=gold&position={position}");
                response.EnsureSuccessStatusCode();
                returnString = await response.Content.ReadAsStringAsync();
                CacheManager.Set(position, returnString);
                return returnString;

            } catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
