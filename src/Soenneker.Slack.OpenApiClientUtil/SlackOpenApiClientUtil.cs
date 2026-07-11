using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Dictionaries.Singletons;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Slack.HttpClients.Abstract;
using Soenneker.Slack.OpenApiClientUtil.Abstract;
using Soenneker.Slack.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;

namespace Soenneker.Slack.OpenApiClientUtil;

///<inheritdoc cref="ISlackOpenApiClientUtil"/>
public sealed class SlackOpenApiClientUtil : ISlackOpenApiClientUtil
{
    private readonly SingletonDictionary<SlackOpenApiClient> _clients;
    private readonly ISlackOpenApiHttpClient _httpClientUtil;
    private readonly IConfiguration _configuration;
    private readonly string _baseUrl;
    private readonly string _authHeaderName;
    private readonly string _authHeaderValueTemplate;

    public SlackOpenApiClientUtil(ISlackOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _httpClientUtil = httpClientUtil;
        _configuration = configuration;
        _baseUrl = configuration["Slack:ClientBaseUrl"] ?? "https://slack.com";
        _authHeaderName = configuration["Slack:AuthHeaderName"] ?? "Authorization";
        _authHeaderValueTemplate = configuration["Slack:AuthHeaderValueTemplate"] ?? "Bearer {token}";
        _clients = new SingletonDictionary<SlackOpenApiClient>(CreateClient);
    }

    private async ValueTask<SlackOpenApiClient> CreateClient(string connectionKey, CancellationToken token)
    {
        (string apiKey, string baseUrl) = ParseConnectionKey(connectionKey);
        HttpClient httpClient = await _httpClientUtil.Get(apiKey, baseUrl, token).NoSync();
        string authHeaderValue = _authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

        var requestAdapter = new HttpClientRequestAdapter(
            new GenericAuthenticationProvider(headerName: _authHeaderName, headerValue: authHeaderValue), httpClient: httpClient)
        {
            BaseUrl = baseUrl
        };

        return new SlackOpenApiClient(requestAdapter);
    }

    public ValueTask<SlackOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return Get(_configuration.GetValueStrict<string>("Slack:ApiKey"), _baseUrl, cancellationToken);
    }

    public ValueTask<SlackOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default)
    {
        return Get(apiKey, _baseUrl, cancellationToken);
    }

    public ValueTask<SlackOpenApiClient> Get(string apiKey, string baseUrl, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);
        ArgumentException.ThrowIfNullOrWhiteSpace(baseUrl);

        string normalizedBaseUrl = new Uri(baseUrl, UriKind.Absolute).AbsoluteUri.TrimEnd('/');

        return _clients.Get(CreateConnectionKey(apiKey, normalizedBaseUrl), cancellationToken);
    }

    private static string CreateConnectionKey(string apiKey, string baseUrl) => string.Concat(apiKey, "\0", baseUrl);

    private static (string ApiKey, string BaseUrl) ParseConnectionKey(string connectionKey)
    {
        int separatorIndex = connectionKey.IndexOf('\0');

        return (connectionKey[..separatorIndex], connectionKey[(separatorIndex + 1)..]);
    }

    public void Dispose()
    {
        _clients.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _clients.DisposeAsync();
    }
}
