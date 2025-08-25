using OpenAI;
using OpenAI.Chat;

namespace AIClients
{
    /// <summary>
    /// Encapsulates interaction with OpenAI's Chat API, maintaining chat history and streaming responses.
    /// </summary>
    public class OpenAIAppClient
    {
        private readonly OpenAIClient _client;
        private readonly ChatClient _chatClient;
        private readonly List<ChatMessage> _chatHistory;

        /// <summary>
        /// Initializes the OpenAI chat client with the specified model.
        /// </summary>
        /// <param name="model">The model to use for chat (default is "gpt-5-nano").</param>
        public OpenAIAppClient(string model = "gpt-5-nano")
        {
            // Initialize OpenAI client using API key (ensure environment variable is set or replace here)
            _client = new OpenAIClient("OPENAI-CLIENT-KEY");
            
            // Create a chat client for the specified model
            _chatClient = _client.GetChatClient(model);

            // Initialize chat history list to maintain context
            _chatHistory = new List<ChatMessage>();

            // [OPTIONAL] Here you can start your with a system message to set the context for the chat
            //_chatHistory = new List<ChatMessage>
            //{
            //    new SystemChatMessage("You are a helpful assistant that can provide weather information.")
            //};
        }

        /// <summary>
        /// Sends a message to the AI and streams the response token by token.
        /// </summary>
        /// <param name="userMessage">The user's message to send.</param>
        /// <param name="onTokenReceived">Optional callback invoked for each token received from the AI, useful for streaming responses in real-time.</param>
        /// <returns>The full AI response as a single string.</returns>
        public async Task<string> SendMessageAsync(string userMessage, Action<string>? onTokenReceived = null)
        {
            // Add the user's message to the chat history to maintain context
            _chatHistory.Add(new UserChatMessage(userMessage));

            // Start streaming the AI response based on the current chat history
            var response = _chatClient.CompleteChatStreamingAsync(_chatHistory);

            // Collect all text tokens as they arrive
            var collectedParts = new List<string>();

            // Iterate asynchronously over the streaming updates
            await foreach (var update in response)
            {
                if (update.ContentUpdate != null)
                {
                    foreach (var part in update.ContentUpdate)
                    {
                        // Only handle textual parts
                        if (part.Kind == ChatMessageContentPartKind.Text)
                        {
                            collectedParts.Add(part.Text);

                            // Invoke the callback for real-time streaming updates (optional)
                            onTokenReceived?.Invoke(part.Text);
                        }
                    }
                }
            }

            // Combine all collected tokens into the final response
            var fullResponse = string.Join("", collectedParts);

            // Add the AI's full message to the chat history for future context
            if (!string.IsNullOrWhiteSpace(fullResponse))
                _chatHistory.Add(new AssistantChatMessage(fullResponse));

            // Return the full response to the caller
            return fullResponse;
        }

        /// <summary>
        /// Exposes a read-only view of the conversation history.
        /// This allows displaying past messages without allowing external modification.
        /// </summary>
        public IReadOnlyList<ChatMessage> ChatHistory => _chatHistory.AsReadOnly();
    }
}
