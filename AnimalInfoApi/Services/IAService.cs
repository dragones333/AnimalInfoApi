using OpenAI.Chat;

namespace AnimalInfoApi.Services
{
    public class IAService : IIAService
    {
        private readonly ChatClient _chat;

        public IAService(IConfiguration config)
        {
            var apiKey = config["OpenAIKey"];

            _chat = new ChatClient(
                model: "gpt-4.1-mini",
                apiKey: apiKey
            );
        }

        public async Task<string> GenerarDescripcion(string animal)
        {
            string prompt = $"Genera una descripción clara, interesante y útil del animal: {animal}.";

            var response = await _chat.CompleteChatAsync(new[]
            {
                new UserChatMessage(prompt)
            });

            return response.Value.Content[0].Text;
        }
    }
}