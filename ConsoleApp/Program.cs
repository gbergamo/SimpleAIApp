using AIClients;

WriteColored("Fake GPT", ConsoleColor.DarkBlue, true);
WriteColored("Simple ChatGPT Console (type 'bye' to quit)\n", ConsoleColor.Gray, true);

const string apiKey = "OPENAI API KEY";
var openAIAppClient = new OpenAIAppClient();

if (string.IsNullOrEmpty(apiKey))
{
    WriteColored("Please set the OPENAI_API_KEY environment variable.", ConsoleColor.Red, true);
    return;
}

var running = true;
while (running)
{
    WriteColored("You: ", ConsoleColor.Green);
    var userMessage = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(userMessage) || userMessage.Equals("bye", StringComparison.OrdinalIgnoreCase))
        break;

    await SendOpenAIMessage(userMessage);

    Console.WriteLine("\n");
}

async Task SendOpenAIMessage(string userMessage)
{
    WriteColored("AI: ", ConsoleColor.Green);
    await openAIAppClient.SendMessageAsync(userMessage, Console.Write);
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