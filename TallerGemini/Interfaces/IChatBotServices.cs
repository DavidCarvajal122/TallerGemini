namespace TallerGemini.Interfaces
{
    public interface IChatBotServices
    {
        public Task<string> GetChatResponse(string prompt);  
    }
}
