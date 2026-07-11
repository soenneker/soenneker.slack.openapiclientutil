using Soenneker.Slack.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Slack.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface ISlackOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<SlackOpenApiClient> Get(CancellationToken cancellationToken = default);

    /// <summary>Gets a client for a specific Slack API token using the configured base URL.</summary>
    ValueTask<SlackOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default);

    /// <summary>Gets a client for a specific Slack tenant connection.</summary>
    ValueTask<SlackOpenApiClient> Get(string apiKey, string baseUrl, CancellationToken cancellationToken = default);
}
