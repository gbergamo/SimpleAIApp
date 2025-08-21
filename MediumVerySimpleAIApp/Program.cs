using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

WriteColored("Fake GPT", ConsoleColor.DarkBlue, true);
WriteColored("Simple ChatGPT Console (type 'bye' to quit)\n", ConsoleColor.Gray, true);

const string apiKey = "OPENAI API KEY";

if (string.IsNullOrEmpty(apiKey))
{
    WriteColored("Please set the OPENAI_API_KEY environment variable.", ConsoleColor.Red, true);
    return;
}

// Create OpenAI client
var client = new OpenAIClient(apiKey);

// Create a chat client (GPT-4o-mini is cheap & fast, you can change model)
var chatClient = client.GetChatClient("gpt-5-nano");

// [OPTIONAL] Here you can start your with a system message to set the context for the chat
//var chatHistory = new List<ChatMessage>
//{
//    new SystemChatMessage("You are a helpful assistant that can provide weather information.")
//};

var chatHistory = new List<ChatMessage>();

var running = true;
while (running)
{
    WriteColored("You: ", ConsoleColor.Green);
    var userMessage = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(userMessage) || userMessage.Equals("bye", StringComparison.OrdinalIgnoreCase))
        break;

    // Add user message to chat history
    chatHistory.Add(new UserChatMessage(userMessage));

    await SendOpenAIMessage();

    Console.WriteLine("\n");
}

async Task SendOpenAIMessage()
{
    // Stream the response
    var response = chatClient.CompleteChatStreamingAsync(chatHistory);
    await WriteResponse(response);
}

async Task WriteResponse(AsyncCollectionResult<StreamingChatCompletionUpdate> response)
{
    WriteColored("AI: ", ConsoleColor.Green);
    var collectedParts = new List<string>();

    await foreach (var msg in response)
    {
        if (msg.ContentUpdate is not null)
        {
            foreach (var part in msg.ContentUpdate)
            {
                if (part.Kind == ChatMessageContentPartKind.Text)
                {
                    Console.Write(part.Text);
                    collectedParts.Add(part.Text);
                }
            }
        }
    }

    if (collectedParts.Count > 0)
    {
        var fullResponse = string.Join("", collectedParts);
        chatHistory.Add(new AssistantChatMessage(fullResponse));
    }
}

static void WriteColored(string text, ConsoleColor color, bool newLine = false)
{
    var originalColor = Console.ForegroundColor;
    Console.ForegroundColor = color;
    
    if (newLine)
        Console.WriteLine(text);
    else
        Console.Write(text);

    Console.ForegroundColor = originalColor;
}