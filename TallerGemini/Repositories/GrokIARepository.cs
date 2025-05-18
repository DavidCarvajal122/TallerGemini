using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using TallerGemini.Interfaces;

namespace TallerGemini.Repositories
{
    public class GrokIARepository : IChatBotServices
    {
        HttpClient _httpClient;
        private string apiKey = "xai-vFMTVHGKDeI682LP6sXuIbK5xbL0Ru3mzds5jr6YA2W66f1LfzHisDjYJFB3NO25Mak2HGQdls7evJB2";
        public GrokIARepository()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }
        public async Task<string> GetChatResponse(string prompt)
        {
            var url = "https://api.x.ai/v1/chat/completions";

            var requestBody = new
            {
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = prompt }
                },
                model = "grok-3-latest",
                stream = false,
                temperature = 0
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }
    }
}
