namespace MTrading;

public interface IQueryPipelineBehavior<in TQuery, TResult> 
    where TQuery : IQuery<TResult>
{
    Task<TResult> Handle(TQuery query, QueryPipelineHandler<TResult> nextHandler);
}