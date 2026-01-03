using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Restaurant.Identification.Application;

public static class Configuration
{

    private static readonly Assembly thisAssembly = typeof(Configuration).Assembly;

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped(thisAssembly, "UseCase");

        return services;
    }


}
