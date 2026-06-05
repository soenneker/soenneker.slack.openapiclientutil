using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Slack.HttpClients.Registrars;
using Soenneker.Slack.OpenApiClientUtil.Abstract;

namespace Soenneker.Slack.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class SlackOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="SlackOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddSlackOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddSlackOpenApiHttpClientAsSingleton()
                .TryAddSingleton<ISlackOpenApiClientUtil, SlackOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="SlackOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddSlackOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddSlackOpenApiHttpClientAsSingleton()
                .TryAddScoped<ISlackOpenApiClientUtil, SlackOpenApiClientUtil>();

        return services;
    }
}
