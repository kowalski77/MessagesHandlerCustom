using Microsoft.Extensions.DependencyInjection;

namespace MTrading;

public abstract class RequestHandlerBase<TResult>
{
    public abstract Task<TResult> Handle(IRequest request, IServiceProvider serviceProvider);
}

public class QueryResultHandler<TQuery, TResult> : RequestHandlerBase<TResult>
    where TQuery : IQuery<TResult>
{
    public override async Task<TResult> Handle(IRequest request, IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

        return await handler.Handle((TQuery)request).ConfigureAwait(false);
    }
}