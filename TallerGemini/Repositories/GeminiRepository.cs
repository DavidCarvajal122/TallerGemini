using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TallerGemini.Models;

namespace TallerGemini.Repositories
{
    public class GeminiRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string apiKey = "AIzaSyA9Z4NGf8-ic-wvdaQ6xHNLI8ZHZkzxvM4";

        public GeminiRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetResponse(string prompt)
        {
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";
            var request = new GeminiRequest
            {
                contents = new List<Content>
                {
                    new Content
                    {
                        parts = new List<Part>
                        {
                            new Part { text = prompt }
                        }
                    }
                }
            };

            var message = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(request)
            };

            var response = await _httpClient.SendAsync(message);
            var responseString = await response.Content.ReadAsStringAsync();

            try
            {
                using var doc = JsonDocument.Parse(responseString);
                var textResponse = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return textResponse ?? "No se recibió contenido.";
            }
            catch (Exception ex)
            {
                return $"Error al procesar la respuesta: {ex.Message}";
            }
        }
    }
}
