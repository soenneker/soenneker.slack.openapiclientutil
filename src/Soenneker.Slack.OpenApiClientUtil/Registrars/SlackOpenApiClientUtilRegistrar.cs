using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Slack.HttpClients.Registrars;
using Soenneker.Slack.OpenApiClientUtil.Abstract;

namespace Soenneker.Slack.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers lazily initialized Slack API clients.
/// </summary>
public static class SlackOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds the Slack API client utility as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddSlackOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddSlackOpenApiHttpClientAsSingleton()
                .TryAddSingleton<ISlackOpenApiClientUtil, SlackOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds the Slack API client utility as a scoped service backed by the singleton HTTP client provider. <para/>
    /// </summary>
    public static IServiceCollection AddSlackOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddSlackOpenApiHttpClientAsSingleton()
                .TryAddScoped<ISlackOpenApiClientUtil, SlackOpenApiClientUtil>();

        return services;
    }
}
