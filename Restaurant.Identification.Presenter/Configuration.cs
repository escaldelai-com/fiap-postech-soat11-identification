using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Restaurant.Identification.Presenter;

public static class Configuration
{

    private static readonly Assembly thisAssembly = typeof(Configuration).Assembly;

    public static IServiceCollection AddPresenter(this IServiceCollection services)
    {
        services.AddAutoMapper(thisAssembly);

        return services;
    }

}
