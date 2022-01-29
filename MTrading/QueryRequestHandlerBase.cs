using Microsoft.Extensions.DependencyInjection;

namespace MTrading;

public abstract class QueryRequestHandlerBase<TResult>
{
    public abstract Task<TResult> Handle(IQuery<TResult> query, IServiceProvider serviceProvider);
}

public class QueryResultHandler<TQuery, TResult> : QueryRequestHandlerBase<TResult>
    where TQuery : IQuery<TResult>
{
    public override async Task<TResult> Handle(IQuery<TResult> query, IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

        return await handler.Handle((TQuery)query).ConfigureAwait(false);
    }
}