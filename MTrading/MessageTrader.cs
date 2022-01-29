using Microsoft.Extensions.DependencyInjection;

namespace MTrading;

public class MessageTrader : IMessageTrader
{
    private readonly IServiceProvider serviceProvider;

    public MessageTrader(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public Task<Result> SendAsync<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        ArgumentNullException.ThrowIfNull(command);

        return CommandHandlerCore.Handle(command, this.serviceProvider);
    }

    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        ArgumentNullException.ThrowIfNull(query);

        var type = typeof(IQueryHandler<,>);
        Type[] typeArgs = { query.GetType(), typeof(TResult) };
        var handlerType = type.MakeGenericType(typeArgs);

        dynamic handler = this.serviceProvider.GetRequiredService(handlerType);
        var result = await handler.Handle((dynamic)query).ConfigureAwait(false);

        return result;
    }
}