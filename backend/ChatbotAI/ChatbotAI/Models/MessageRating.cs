namespace ChatbotAI.Models
{
    public class MessageRating
    {
        public int Id { get; set; }
        public int ChatMessageId { get; set; }
        public ChatMessage? ChatMessage { get; set; }
        public bool? IsPositive { get; set; }
    }
}
