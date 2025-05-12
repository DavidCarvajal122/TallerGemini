using TallerGemini.Interfaces;

namespace TallerGemini.Repositories
{
    public class OpenAiRepository : IChatBotServices
    {
        public Task<string> GetChatResponse(string prompt)
        {
            throw new NotImplementedException();
        }
    }
}
