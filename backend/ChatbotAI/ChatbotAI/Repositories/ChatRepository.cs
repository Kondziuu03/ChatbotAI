using ChatbotAI.Db;
using ChatbotAI.Exceptions;
using ChatbotAI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatbotAI.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;

        public ChatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ChatMessage> CreateMessageAsync(string userMessage, string botResponse)
        {
            var message = new ChatMessage
            {
                UserMessage = userMessage,
                BotResponse = botResponse
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            return message;
        }

        public async Task<List<ChatMessage>> GetMessagesAsync()
        {
            return await _context.ChatMessages
                .Include(m => m.Rating)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task UpdateMessageRating(int messageId, bool? isPositive)
        {
            var message = await GetMessage(messageId);

            if (message == null)
                throw new NotFoundException($"Could not find message with id: {messageId}");

            if (message.Rating == null)
                message.Rating = CreateMessageRating(message, isPositive);
            else
                message.Rating.IsPositive = isPositive;

            await _context.SaveChangesAsync();
        }
        private async Task<ChatMessage?> GetMessage(int id)
        {
            return await _context.ChatMessages
                .Include(m => m.Rating)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        private MessageRating CreateMessageRating(ChatMessage message, bool? isPositive)
            => new MessageRating
            {
                ChatMessage = message,
                IsPositive = isPositive
            };
    }
}
