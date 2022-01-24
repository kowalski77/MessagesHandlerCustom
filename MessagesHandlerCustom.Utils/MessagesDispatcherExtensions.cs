using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MessagesHandlerCustom.Utils;

public static class MessagesDispatcherExtensions
{
    public static IServiceCollection AddMessagesDispatcherFromAssembly<T>(
        this IServiceCollection services) where T : class
    {
        ArgumentNullException.ThrowIfNull(services);

        RegisterDispatchersFromAssembly(services, typeof(T).Assembly);

        services.AddScoped<MessagesDispatcher>();

        return services;
    }

    private static IServiceCollection RegisterDispatchersFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var commandHandlerImplementations = assembly.GetTypes()
            .Where(p => p.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
            .ToList();

        foreach (var type in commandHandlerImplementations)
        {
            var interfaceImplemented = type.GetInterfaces().First();
            services.Add(new ServiceDescriptor(interfaceImplemented, type, ServiceLifetime.Scoped));
        }

        return services;
    }
}