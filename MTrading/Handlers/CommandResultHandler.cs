using Microsoft.Extensions.DependencyInjection;

namespace MTrading;

internal sealed class CommandResultHandler<TCommand> : RequestHandler<Result>
    where TCommand : ICommand
{
    public override async Task<Result> Handle(IRequest request, IServiceProvider serviceProvider)
    {
        Task<Result> Handler() => serviceProvider.GetRequiredService<ICommandHandler<TCommand>>().Handle((TCommand)request);

        var handlers = serviceProvider.GetServices<ICommandPipelineBehavior<TCommand>>()
            .Reverse()
            .Aggregate((CommandPipelineHandler)Handler, (next, pipeline) => () => pipeline.Handle((TCommand)request, next))();

        return await handlers.ConfigureAwait(false);
    }
}