using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MTrading;

public static class MessagesDispatcherExtensions
{
    public static IServiceCollection AddMessagesDispatcherFromAssembly<T>(
        this IServiceCollection services) where T : class
    {
        ArgumentNullException.ThrowIfNull(services);

        services.RegisterDispatchersFromAssembly(typeof(T).Assembly);

        services.AddScoped<IMessagesDispatcher, MessagesDispatcher>();

        return services;
    }

    private static IServiceCollection RegisterDispatchersFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var handlerImplementations = assembly.GetTypes()
            .Where(p => p.GetInterfaces().Any(i => i.IsGenericType &&
                                                   (i.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                                                    i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))).ToList();

        foreach (var type in handlerImplementations)
        {
            var interfaceImplemented = type.GetInterfaces().First();
            services.Add(new ServiceDescriptor(interfaceImplemented, type, ServiceLifetime.Scoped));
        }

        return services;
    }
}