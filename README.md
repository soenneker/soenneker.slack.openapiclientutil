[![](https://img.shields.io/nuget/v/soenneker.slack.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.slack.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.slack.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.slack.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.slack.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.slack.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.slack.openapiclientutil/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.slack.openapiclientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Slack.OpenApiClientUtil

Provides lazily initialized Slack Web API clients for conversations, messages, files, users, teams, reactions, apps, workflows, and administration across one or more workspaces.

## Installation

```bash
dotnet add package Soenneker.Slack.OpenApiClientUtil
```

## Configuration

```json
{
  "Slack": {
    "ApiKey": "xoxb-your-bot-token"
  }
}
```

## Usage

```csharp
using Soenneker.Slack.OpenApiClient.Models;
using Soenneker.Slack.OpenApiClientUtil.Abstract;
using Soenneker.Slack.OpenApiClientUtil.Registrars;

services.AddSlackOpenApiClientUtilAsSingleton();

var client = await slackClientUtil.Get(cancellationToken);
AuthTestResponse? identity = await client.Api.AuthTest.PostAsync(
    new AuthTestRequest(),
    cancellationToken: cancellationToken);
```

The parameterless `Get()` uses `Slack:ApiKey` and `Slack:ClientBaseUrl`. Pass connection values explicitly to work with multiple Slack tenants:

```csharp
SlackOpenApiClient tenantClient = await slackOpenApiClientUtil.Get(tenantApiKey, tenantBaseUrl);
```

Generated clients are cached per token and base URL within the utility. `AddSlackOpenApiClientUtilAsScoped()` creates a separate generated-client cache per scope while retaining the singleton authenticated HTTP client provider.
