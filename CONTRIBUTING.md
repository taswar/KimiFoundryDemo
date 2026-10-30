# Contributing

Thanks for helping improve this sample. It's a small .NET 10 console app that asks Kimi K2.7 Code on Microsoft Foundry to review a buggy C# method, using the `Azure.AI.Inference` SDK and Entra ID. Contributions that make it clearer, more correct, or more useful to other .NET developers are welcome.

## Ways to contribute

- **Report a bug.** The call fails, or the README doesn't match what the code does.
- **Fix the docs.** Typos, unclear steps, or a missing troubleshooting case (for example, an endpoint format that didn't work for you).
- **Improve the sample.** For example, more code to review, streaming output, or the async call that's commented out in `Program.cs`.
- **Share results.** If you reviewed your own code with Kimi K2.7 Code, an issue describing what it caught or missed helps others.

For anything larger than a small fix, open an issue first so we can agree on the approach before you write code.

## Set up

1. Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) and the Azure CLI.
2. Clone the repo and build it:

   ```bash
   git clone https://github.com/taswar/KimiFoundryDemo.git
   cd KimiFoundryDemo
   dotnet build
   ```

3. To run it, deploy Kimi K2.7 Code on a Microsoft Foundry resource, set `AZURE_AI_CHAT_ENDPOINT` (and `AZURE_AI_MODEL` if your deployment isn't named `Kimi-K2.7-Code`), and sign in with `az login`, as described in the [README](README.md#configuration). Your account needs the **Cognitive Services User** role on the resource. The sample uses Entra ID only, with no API key.

## Guidelines

- **Never commit secrets.** No keys, endpoints, tenant IDs or tokens in code, config files or screenshots.
- **Keep the token scope.** Foundry endpoints need a token for `https://cognitiveservices.azure.com/.default`. Keep `ScopeOverrideCredential`, or explain why it's no longer needed.
- **Keep Entra ID.** The sample uses `DefaultAzureCredential` with no API key. Keep it that way.
- **Keep the code under review buggy.** The `GetUserAsync` method in the prompt is wrong on purpose, so the model has something real to find. Don't fix it, and if you add code to review, say in the pull request which bugs it contains.
- **Keep it a sample.** The code should stay short and readable in one `Program.cs`. Prefer clarity over abstraction, and add a dependency only when it clearly helps.
- **Match the existing style.** Target `net10.0`, keep nullable reference types on, and follow the layout already in `Program.cs`.
- **Keep the README in sync.** If you change the prompt, settings or output, update the README in the same pull request.
- **One change per pull request.** Small, focused pull requests are easier to review and merge.

## Pull request checklist

- [ ] `dotnet build` succeeds with no new warnings.
- [ ] The app runs against your own Kimi K2.7 Code deployment, or you explain why you couldn't run it.
- [ ] No secrets, endpoints or personal data are in the diff.
- [ ] The README reflects any change in the prompt, settings or output.
- [ ] The pull request description says what changed and why.

The build workflow runs on every pull request and has to pass before a merge.

## Reporting a bug

Open an issue and include:

- What you expected and what happened.
- The full error message, with any endpoints removed.
- Your .NET SDK version (`dotnet --version`) and operating system.
- Your deployment name and the endpoint format you used (with the resource name removed).

## Security issues

Please don't report security problems in a public issue. Use **Security → Report a vulnerability** on the repo to send a private report.

## License

By contributing, you agree that your contributions are licensed under the [MIT License](LICENSE) that covers this project.
