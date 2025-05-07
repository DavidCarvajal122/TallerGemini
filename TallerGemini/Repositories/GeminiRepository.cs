using TallerGemini.Interfaces;
using TallerGemini.Models;

namespace TallerGemini.Repositories
{
    public class GeminiRepository : IChatBotServices
    {
        HttpClient _httpClient;
        private string apiKey = "AIzaSyA9Z4NGf8-ic-wvdaQ6xHNLI8ZHZkzxvM4"; 
        public GeminiRepository()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> GetChatResponse(string prompt)
        {
            string url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + apiKey;
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url);

            GeminiRequest request = new GeminiRequest
            {
                contents = new List<Content>
                {
                    new Content
                    {
                        parts = new List<Part>
                        {
                            new Part
                            {
                              text =prompt
                            }
                            
                        }
                    }
                } 
            };
            message.Content = JsonContent.Create<GeminiRequest>(request);
            var response = await _httpClient.SendAsync(message);
            string answer = await response.Content.ReadAsStringAsync();

            return answer;
        }
    }
}
