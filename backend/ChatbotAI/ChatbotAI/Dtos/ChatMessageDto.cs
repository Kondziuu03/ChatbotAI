namespace ChatbotAI.Dtos
{
    public class ChatMessageDto
    {
        public int Id { get; set; }
        public string UserMessage { get; set; } = string.Empty;
        public string BotResponse { get; set; } = string.Empty;
        public bool? Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
