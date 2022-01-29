using Microsoft.Extensions.DependencyInjection;

namespace MTrading;

internal sealed class QueryResultHandler<TQuery, TResult> : RequestHandlerBase<TResult>
    where TQuery : IQuery<TResult>
{
    public override async Task<TResult> Handle(IRequest request, IServiceProvider serviceProvider)
    {
        var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

        return await handler.Handle((TQuery)request).ConfigureAwait(false);
    }
}