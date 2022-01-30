using Microsoft.Extensions.DependencyInjection;

namespace MTrading;

internal sealed class QueryHandler<TQuery, TResult> : RequestHandler<TResult>
    where TQuery : IQuery<TResult>
{
    public override async Task<TResult> Handle(IRequest request, IServiceProvider serviceProvider)
    {
        Task<TResult> Handler() => serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>().Handle((TQuery)request);

        var handlers = serviceProvider.GetServices<IQueryPipelineBehavior<TQuery, TResult>>()
            .Reverse()
            .Aggregate((QueryPipelineHandler<TResult>)Handler, (next, pipeline) => () => pipeline.Handle((TQuery)request, next))();

        return await handlers.ConfigureAwait(false);
    }
}