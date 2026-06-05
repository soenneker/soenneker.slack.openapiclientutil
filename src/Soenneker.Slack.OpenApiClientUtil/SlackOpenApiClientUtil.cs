using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Slack.HttpClients.Abstract;
using Soenneker.Slack.OpenApiClientUtil.Abstract;
using Soenneker.Slack.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Slack.OpenApiClientUtil;

///<inheritdoc cref="ISlackOpenApiClientUtil"/>
public sealed class SlackOpenApiClientUtil : ISlackOpenApiClientUtil
{
    private readonly AsyncSingleton<SlackOpenApiClient> _client;

    public SlackOpenApiClientUtil(ISlackOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<SlackOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Slack:ApiKey");
            string authHeaderValueTemplate = configuration["Slack:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new SlackOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<SlackOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
