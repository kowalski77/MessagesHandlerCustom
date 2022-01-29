using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace MTrading;

public sealed class CommandHandlerCore
{
    public static Task<Result> Handle<TCommand>([DisallowNull] TCommand command, IServiceProvider serviceProvider)
        where TCommand : ICommand
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(serviceProvider);

        Task<Result> Handler() => serviceProvider.GetRequiredService<ICommandHandler<TCommand>>().Handle(command);

        var handlers = serviceProvider.GetServices<ICommandPipelineBehavior<TCommand>>()
            .Reverse()
            .Aggregate((CommandPipelineHandler)Handler, (next, pipeline) => () => pipeline.Handle(command, next))();

        return handlers;
    }
}