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
}
