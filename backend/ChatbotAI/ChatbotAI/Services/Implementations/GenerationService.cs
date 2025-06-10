using ChatbotAI.Services.Interfaces;

namespace ChatbotAI.Services.Implementations
{
    public class GenerationService : IGenerationService
    {
        private readonly string[] DefaultResponses =
        [
            "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
            "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
            "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in",
            "the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing",
        ];

        private readonly Random _random = new();

        public string GenerateResponse(string userMessage)
        {
            //integrate LLM model
            //prepare chat history
            //generate response using LLM

            return DefaultResponses[_random.Next(DefaultResponses.Length)];
        }
    }
}
