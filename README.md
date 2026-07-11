[![](https://img.shields.io/nuget/v/soenneker.slack.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.slack.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.slack.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.slack.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.slack.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.slack.openapiclientutil/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Slack.OpenApiClientUtil
### A thread-safe utility for obtaining Slack's OpenApiClient singleton.

## Installation

```
dotnet add package Soenneker.Slack.OpenApiClientUtil
```

The parameterless `Get()` uses `Slack:ApiKey` and `Slack:ClientBaseUrl`. Pass connection values explicitly to work with multiple Slack tenants:

```csharp
SlackOpenApiClient tenantClient = await slackOpenApiClientUtil.Get(tenantApiKey, tenantBaseUrl);
```
