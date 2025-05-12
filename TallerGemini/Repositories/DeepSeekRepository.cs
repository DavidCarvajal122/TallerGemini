using TallerGemini.Interfaces;

namespace TallerGemini.Repositories
{
    public class DeepSeekRepository : IChatBotServices
    {
        public async Task<string> GetChatResponse(string prompt)
        {
            return "Este es un string de Deep Seek"; 
        }
    }
}
