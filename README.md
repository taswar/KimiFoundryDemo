# KimiFoundryDemo

Blog Post: [Kimi K2.7 Code is Now Available in Microsoft Foundry](https://taswar.zeytinsoft.com/kimi-k2-7-code-is-now-available-in-microsoft-foundry/)

A minimal .NET console app that calls a chat model (default: `Kimi-K2.7-Code`) hosted on **Azure AI Foundry** using the `Azure.AI.Inference` SDK with Entra ID (passwordless) authentication.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Azure CLI](https://learn.microsoft.com/cli/azure/install-azure-cli)
- An Azure AI Foundry resource with a deployed model
- The **Cognitive Services User** (or **Azure AI Developer**) role assigned to your identity on the Foundry resource

## Required libraries

The app depends on the following NuGet packages (already referenced in `KimiFoundryDemo.csproj`):

| Package | Version |
| --- | --- |
| `Azure.AI.Inference` | `1.0.0-beta.5` |
| `Azure.Core` | `1.60.0` |
| `Azure.Identity` | `1.21.0` |

To add them to a fresh project:

```powershell
dotnet add package Azure.AI.Inference --version 1.0.0-beta.5
dotnet add package Azure.Core --version 1.60.0
dotnet add package Azure.Identity --version 1.21.0
```

To restore the packages for this project:

```powershell
dotnet restore
```

## Configuration

The app reads two environment variables:

| Variable | Required | Description |
| --- | --- | --- |
| `AZURE_AI_CHAT_ENDPOINT` | Yes | The Foundry models endpoint, e.g. `https://<your-resource>.services.ai.azure.com/models` |
| `AZURE_AI_MODEL` | No | Model deployment name. Defaults to `Kimi-K2.7-Code`. |

## How to run

1. Sign in with the Azure CLI (used by `DefaultAzureCredential`):

   ```powershell
   az login
   ```

2. Set the endpoint (and optionally the model) for the current terminal session:

   ```powershell
   $env:AZURE_AI_CHAT_ENDPOINT = "https://<your-resource>.services.ai.azure.com/models"
   $env:AZURE_AI_MODEL = "Kimi-K2.7-Code"
   ```

3. Run the app:

   ```powershell
   dotnet run
   ```

You should see the model's code review printed to the console.

## Authentication notes

- Authentication uses `DefaultAzureCredential` with `ExcludeManagedIdentityCredential = true`, so it does **not** probe the Azure IMDS endpoint (`169.254.169.254`) when running locally.
- Foundry (Cognitive Services) endpoints require a token whose audience is `https://cognitiveservices.azure.com`. The app wraps the credential (`ScopeOverrideCredential`) to force this scope.

## Troubleshooting

| Error | Cause | Fix |
| --- | --- | --- |
| `Set AZURE_AI_CHAT_ENDPOINT environment variable.` | Endpoint not set | Set `AZURE_AI_CHAT_ENDPOINT` in the current terminal |
| `ManagedIdentityCredential ... IMDS ... 169.254.169.254` | Running locally without sign-in | Run `az login` |
| `401 Unauthorized ... audience is incorrect` | Wrong token audience or missing role | Ensure the scope override is present and your identity has the **Cognitive Services User** role |
