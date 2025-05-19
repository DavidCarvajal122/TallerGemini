using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TallerGemini.Interfaces;

namespace TallerGemini.Repositories
{
    public class DeepSeekRepository : IChatBotServices
    {
        private readonly HttpClient _httpClient;
        private readonly string apiKey = "sk-22a350dd1f914fedbff1672861b17eb9";

        public DeepSeekRepository()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> GetGeminiResponse(string prompt)
        {
            return await SendChatRequest(prompt);
        }

        public async Task<string> GetGroqResponse(string prompt)
        {
            return await SendChatRequest(prompt);
        }

        private async Task<string> SendChatRequest(string prompt)
        {
            var url = "https://api.deepseek.com/chat/completions";

            var body = new
            {
                model = "deepseek-chat",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = prompt }
                },
                stream = false
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return $"Error: {responseBody}";
            }

            try
            {
                using var doc = JsonDocument.Parse(responseBody);
                var contentText = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return contentText ?? "No se recibió contenido.";
            }
            catch (Exception ex)
            {
                return $"Error al procesar la respuesta: {ex.Message}";
            }
        }
    }
}


