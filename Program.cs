using Azure.Identity;
using Azure;
using Azure.AI.Inference;
using Azure.Core;


var endpoint = Environment.GetEnvironmentVariable("AZURE_AI_CHAT_ENDPOINT")
    ?? throw new InvalidOperationException("Set AZURE_AI_CHAT_ENDPOINT environment variable.");

var model = Environment.GetEnvironmentVariable("AZURE_AI_MODEL") ?? "Kimi-K2.7-Code";

// DefaultAzureCredential handles Entra ID auth — no API key in code.
// Sign in first with: az login
var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
{
    ExcludeManagedIdentityCredential = true
});

// Foundry (Cognitive Services) endpoints require a token whose audience is
// https://cognitiveservices.azure.com. Force that scope regardless of the
// scope the Azure.AI.Inference SDK requests by default.
var scopedCredential = new ScopeOverrideCredential(
    credential, "https://cognitiveservices.azure.com/.default");

var client = new ChatCompletionsClient(new Uri(endpoint), scopedCredential);

var options = new ChatCompletionsOptions
{
    Model = model,
    Temperature = 0.2f,
    MaxTokens = 2000
};

options.Messages.Add(new ChatRequestSystemMessage(
    """
    You are a senior .NET architect.
    Your job is to review code and identify issues clearly and concisely.
    Be direct. Prioritize correctness over style.
    Return findings as a numbered list with severity: critical, warning, or info.
    """));

options.Messages.Add(new ChatRequestUserMessage(
    """
    Review the following C# method and identify any issues:
 
    public async Task GetUserAsync(int userId)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == userId)
            .FirstOrDefault();
 
        return user;
    }
    """));

Console.WriteLine($"Calling model: {model}");
Console.WriteLine("---");

Response<ChatCompletions> response = client.Complete(options);
System.Console.WriteLine(response.Value.Content);

// var response = await client.CompleteAsync(options);

// foreach (var content in response.Value.Content)
// {
//     Console.WriteLine(content.Text);
// }

// Wraps a TokenCredential and forces a fixed audience/scope, ignoring the
// scope the client pipeline asks for.
sealed class ScopeOverrideCredential : TokenCredential
{
    private readonly TokenCredential _inner;
    private readonly string[] _scopes;

    public ScopeOverrideCredential(TokenCredential inner, string scope)
    {
        _inner = inner;
        _scopes = new[] { scope };
    }

    public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
        => _inner.GetToken(new TokenRequestContext(_scopes), cancellationToken);

    public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
        => _inner.GetTokenAsync(new TokenRequestContext(_scopes), cancellationToken);
}