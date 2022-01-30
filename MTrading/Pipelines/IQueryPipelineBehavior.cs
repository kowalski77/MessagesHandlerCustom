namespace MTrading;

public interface IQueryPipelineBehavior<in TRequest, TResult> 
    where TRequest : IQuery<TResult>
{
    Task<TResult> Handle(TRequest request, QueryPipelineHandler<TResult> nextHandler);
}