namespace MTrading;

internal abstract class RequestHandlerBase<TResult>
{
    public abstract Task<TResult> Handle(IRequest request, IServiceProvider serviceProvider);
}