using TallerGemini.Interfaces;
using TallerGemini.Repositories;

namespace TallerGemini.Services
{
 
        public class ChatBotServices : IChatBotServices
        {
            public async Task<string> GetGeminiResponse(string prompt)
            {
                var geminiRepo = new GeminiRepository();
                return await geminiRepo.GetResponse(prompt);
            }

            public async Task<string> GetGroqResponse(string prompt)
            {
                var groqRepo = new GroqIARepository();
                return await groqRepo.GetResponse(prompt);
            }
        }
    }

