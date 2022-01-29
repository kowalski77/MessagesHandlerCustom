using Microsoft.Extensions.DependencyInjection;

namespace MTrading;

internal abstract class RequestHandlerBase<TResult>
{
    public abstract Task<TResult> Handle(IRequest request, IServiceProvider serviceProvider);
}

internal sealed class QueryResultHandler<TQuery, TResult> : RequestHandlerBase<TResult>
    where TQuery : IQuery<TResult>
{
    public override async Task<TResult> Handle(IRequest request, IServiceProvider serviceProvider)
    {
        var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

        return await handler.Handle((TQuery)request).ConfigureAwait(false);
    }
}

internal sealed class CommandResultHandler<TCommand> : RequestHandlerBase<Result>
    where TCommand : ICommand
{
    public override async Task<Result> Handle(IRequest request, IServiceProvider serviceProvider)
    {
        var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        return await handler.Handle((TCommand)request).ConfigureAwait(false);
    }
}