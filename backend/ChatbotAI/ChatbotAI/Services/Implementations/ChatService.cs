using ChatbotAI.Dtos;
using ChatbotAI.Repositories;
using ChatbotAI.Services.Interfaces;

namespace ChatbotAI.Services.Implementations
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IGenerationService _generationService;

        public ChatService(IChatRepository chatRepository, IGenerationService generationService)
        {
            _chatRepository = chatRepository;
            _generationService = generationService;
        }

        public async Task<ChatMessageDto> SendMessageAsync(string userMessage)
        {
            var response = _generationService.GenerateResponse(userMessage);

            var message = await _chatRepository.CreateMessageAsync(userMessage, response);

            return new ChatMessageDto
            {
                Id = message.Id,
                UserMessage = message.UserMessage,
                BotResponse = message.BotResponse,
                Rating = null,
                CreatedAt = message.CreatedAt
            };
        }

        public async Task<List<ChatMessageDto>> GetChatHistoryAsync()
        {
            var messages = await _chatRepository.GetMessagesAsync();

            return messages.Select(m =>
                new ChatMessageDto
                {
                    Id = m.Id,
                    UserMessage = m.UserMessage,
                    BotResponse = m.BotResponse,
                    Rating = m.Rating != null ? m.Rating.IsPositive : null,
                    CreatedAt = m.CreatedAt
                }).ToList();
        }

        public async Task RateResponseAsync(int messageId, bool? isPositive)
            => await _chatRepository.UpdateMessageRating(messageId, isPositive);
    }
}
