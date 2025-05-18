using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TallerGemini.Interfaces;

namespace TallerGemini.Repositories
{
    public class GroqIARepository : IChatBotServices
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "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";
        private const string GroqUrl = "https://api.groq.com/openai/v1/chat/completions";

        public GroqIARepository()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
        }

        public async Task<string> GetChatResponse(string prompt)
        {
            var requestBody = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(GroqUrl, content);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(responseString);
                var reply = doc.RootElement
                               .GetProperty("choices")[0]
                               .GetProperty("message")
                               .GetProperty("content")
                               .GetString();

                return reply ?? "Respuesta vacía del modelo.";
            }
            catch (Exception ex)
            {
                return $"Error al obtener respuesta del modelo: {ex.Message}";
            }
        }
    }
}
