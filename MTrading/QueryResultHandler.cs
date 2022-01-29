using Microsoft.Extensions.DependencyInjection;

namespace MTrading;

internal sealed class QueryResultHandler<TQuery, TResult> : RequestHandlerWrapper<TResult>
    where TQuery : IQuery<TResult>
{
    public override async Task<TResult> Handle(IRequest request, IServiceProvider serviceProvider)
    {
        Task<TResult> Handler() => serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>().Handle((TQuery)request);

        var handlers = serviceProvider.GetServices<IRequestPipelineBehavior<TQuery, TResult>>()
            .Reverse()
            .Aggregate((PipelineHandler<TResult>)Handler, (next, pipeline) => () => pipeline.Handle((TQuery)request, next))();

        return await handlers.ConfigureAwait(false);
    }
}