namespace MTrading;

internal abstract class RequestHandler<TResult> : RequestHandlerBase
{
    public abstract Task<TResult> Handle(IRequest request, IServiceProvider serviceProvider);
}