namespace TallerGemini.Interfaces
{
    public interface IChatBotServices
    {

        Task<string> GetGeminiResponse(string prompt);
        Task<string> GetGroqResponse(string prompt);
    }
}
