using System.Reflection;
using MedistR.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MedistR.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMedistR(this IServiceCollection services, params Assembly[] assemblies)
    {
        var handlerInterfaces = new[]
        {
            typeof(ICommandHandler<,>),
        };

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .ToList();

            foreach (var type in types)
            {
                foreach (var @interface in type.GetInterfaces())
                {
                    if (@interface.IsGenericType && handlerInterfaces.Contains(@interface.GetGenericTypeDefinition()))
                    {
                        services.AddTransient(@interface, type);
                    }
                }
            }
        }

        services.AddScoped<IMedistR, MedistR>();
        return services;
    }
}