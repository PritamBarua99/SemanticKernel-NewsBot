using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using OllamaSharp;
using NewsPlugin;

var builder = Kernel.CreateBuilder();

builder.Services.AddScoped<IOllamaApiClient>(_ => new OllamaApiClient("http://localhost:11434"));

builder.Services.AddScoped<IChatCompletionService, OllamaChatCompletionService>();

var kernel = builder.Build();

var chatService = kernel.GetRequiredService<IChatCompletionService>();

builder.Plugins.AddFromType<NewsPlugin.NewsPlugin>();


var history = new ChatHistory();
history.AddSystemMessage("Your name is Bot News. You are help full assistant that will help you with your questions. You specialise in telling news to the users."
    + "Ask the user which category of news they want first, before showing the news." + "You can also write the news to a file if the user asks for it."
    );


while (true)
{
    Console.Write("You: ");
    var userMessage = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(userMessage))
    {
        break;
    }

    history.AddUserMessage(userMessage);

    var response = await chatService.GetChatMessageContentAsync(history);

    Console.WriteLine($"Bot: {response.Content}");

    history.AddMessage(response.Role, response.Content ?? string.Empty);
}
