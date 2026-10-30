# Kimi K2.7 Code C# Demo on Microsoft Foundry

![Kimi K2.7 Code is now available in Microsoft Foundry](https://taswar.zeytinsoft.com/wp-content/uploads/2026/07/Kimi-K2.7-Code-is-Now-Available-in-Microsoft-Foundry-v2.png)

[![Build](https://github.com/taswar/KimiFoundryDemo/actions/workflows/dotnet.yml/badge.svg)](https://github.com/taswar/KimiFoundryDemo/actions/workflows/dotnet.yml)
![.NET 10](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen)](CONTRIBUTING.md)

- 📖 Full walkthrough: [Kimi K2.7 Code is Now Available in Microsoft Foundry](https://taswar.zeytinsoft.com/kimi-k2-7-code-is-now-available-in-microsoft-foundry/)
- ⭐ If this sample helps you, star the repo so other .NET developers can find it.
- 🔁 Related: [Grok 4.7 C# Demo](https://github.com/taswar/Grok-4.7-Demo-CSharp), another coding model on Foundry with a refactoring review
- For more C# samples using other AI models, see [AI models for C# developers](https://taswar.zeytinsoft.com/foundry-ai-models-csharp/)
- For my free ebook on prompt engineering, see [Prompt Engineering for .NET Developers](https://github.com/taswar/PromptEngineeringForDotNetDevelopers)

A minimal .NET 10 console app that asks **Kimi K2.7 Code**, Moonshot AI's coding model hosted on **Microsoft Foundry**, to review a buggy C# method. It uses the `Azure.AI.Inference` SDK with Entra ID (passwordless) authentication.

## Quick start

```powershell
git clone https://github.com/taswar/KimiFoundryDemo.git
cd KimiFoundryDemo
az login
$env:AZURE_AI_CHAT_ENDPOINT = "https://<your-resource>.services.ai.azure.com/models"
dotnet run
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Azure CLI](https://learn.microsoft.com/cli/azure/install-azure-cli)
- A Microsoft Foundry resource with a Kimi K2.7 Code deployment
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

You should see the model's code review printed to the console: a numbered list of findings, each marked critical, warning or info. The method under review awaits `FirstOrDefault()` instead of `FirstOrDefaultAsync()` and declares `Task` while returning a user, so there is something real to find.

## Authentication notes

- Authentication uses `DefaultAzureCredential` with `ExcludeManagedIdentityCredential = true`, so it does **not** probe the Azure IMDS endpoint (`169.254.169.254`) when running locally.
- Foundry (Cognitive Services) endpoints require a token whose audience is `https://cognitiveservices.azure.com`. The app wraps the credential (`ScopeOverrideCredential`) to force this scope.

## Troubleshooting

| Error | Cause | Fix |
| --- | --- | --- |
| `Set AZURE_AI_CHAT_ENDPOINT environment variable.` | Endpoint not set | Set `AZURE_AI_CHAT_ENDPOINT` in the current terminal |
| `ManagedIdentityCredential ... IMDS ... 169.254.169.254` | Running locally without sign-in | Run `az login` |
| `401 Unauthorized ... audience is incorrect` | Wrong token audience or missing role | Ensure the scope override is present and your identity has the **Cognitive Services User** role |

## Project layout

```
Program.cs                    Client setup, the review prompt and the scope override
KimiFoundryDemo.csproj        .NET 10 project and package references
CONTRIBUTING.md               How to contribute
.github/workflows/dotnet.yml  Build workflow
LICENSE                       MIT license
```

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE).
