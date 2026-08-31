using Soenneker.Slack.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Slack.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides lazily initialized Slack API clients for one or more workspaces.
/// </summary>
public interface ISlackOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>Gets the client configured by <c>Slack:ApiKey</c> and <c>Slack:ClientBaseUrl</c>.</summary>
    ValueTask<SlackOpenApiClient> Get(CancellationToken cancellationToken = default);

    /// <summary>Gets a client for a specific Slack API token using the configured base URL.</summary>
    ValueTask<SlackOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default);

    /// <summary>Gets a client for a specific Slack tenant connection.</summary>
    ValueTask<SlackOpenApiClient> Get(string apiKey, string baseUrl, CancellationToken cancellationToken = default);

    /// <summary>Releases every generated Slack client owned by this utility.</summary>
    new void Dispose();

    /// <summary>Asynchronously releases every generated Slack client owned by this utility.</summary>
    new ValueTask DisposeAsync();
}
