using AIClients;

var builder = WebApplication.CreateSlimBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()   // allow all origins
            .AllowAnyMethod()   // allow GET, POST, etc.
            .AllowAnyHeader();  // allow custom headers
    });
});

var app = builder.Build();

app.UseCors(); // enable CORS globally

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var openAIAppClient = new OpenAIAppClient();

app.MapGet("/chat", async (HttpContext context, string message) =>
{
    context.Response.Headers.Append("Content-Type", "text/event-stream");
    context.Response.Headers.Append("Cache-Control", "no-cache");
    context.Response.Headers.Append("Connection", "keep-alive");

    await openAIAppClient.SendMessageAsync(message, async token =>
    {
        await context.Response.WriteAsync($"data: {token}\n\n");
        await context.Response.Body.FlushAsync();
    });
})
.WithName("Chat");

app.MapGet("/redoc", async context =>
{
    var html = """
    <!DOCTYPE html>
    <html>
      <head>
        <title>ReDoc</title>
        <!-- ReDoc script -->
        <script src="https://cdn.redoc.ly/redoc/latest/bundles/redoc.standalone.js"></script>
      </head>
      <body>
        <redoc spec-url='/openapi/v1.json'></redoc>
        <script>
          Redoc.init('/openapi/v1.json');
        </script>
      </body>
    </html>
    """;

    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(html);
});

app.Run();