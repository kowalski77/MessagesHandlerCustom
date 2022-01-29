namespace MTrading;

public interface IRequestPipelineBehavior<in TRequest, TResult> 
    where TRequest : IQuery<TResult>
{
    Task<TResult> Handle(TRequest request, PipelineHandler<TResult> nextHandler);
}