using ChatbotAI.Dtos;
using ChatbotAI.Models;

namespace ChatbotAI.Repositories
{
    public interface IChatRepository
    {
        Task<ChatMessage> CreateMessageAsync(string userMessage, string botResponse);
        Task<List<ChatMessage>> GetMessagesAsync();
        Task UpdateMessageRating(int messageId, bool? isPositive);
    }
}
