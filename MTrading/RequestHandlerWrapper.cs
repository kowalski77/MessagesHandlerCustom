namespace MTrading;

internal abstract class RequestHandlerWrapper<TResult> : RequestHandlerBase
{
    public abstract Task<TResult> Handle(IRequest request, IServiceProvider serviceProvider);
}