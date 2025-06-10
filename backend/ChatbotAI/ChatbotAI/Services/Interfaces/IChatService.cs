using ChatbotAI.Dtos;

namespace ChatbotAI.Services.Interfaces
{
    public interface IChatService
    {
        Task<ChatMessageDto> SendMessageAsync(string userMessage);
        Task<List<ChatMessageDto>> GetChatHistoryAsync();
        Task RateResponseAsync(int messageId, bool? isPositive);
    }
}
