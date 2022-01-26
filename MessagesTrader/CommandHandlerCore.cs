using Microsoft.Extensions.DependencyInjection;

namespace MessagesTrader;

public sealed class CommandHandlerCore
{
    public static Task<Result> Handle<TCommand>(TCommand command, IServiceProvider serviceProvider)
        where TCommand : ICommand
    {
        Task<Result> Handler() => serviceProvider.GetRequiredService<ICommandHandler<TCommand>>().Handle(command);

        var handlers = serviceProvider.GetServices<ICommandPipelineBehavior<TCommand>>()
            .Reverse()
            .Aggregate((CommandPipelineHandler)Handler, (next, pipeline) => () => pipeline.Handle(command, next))();

        return handlers;
    }
}