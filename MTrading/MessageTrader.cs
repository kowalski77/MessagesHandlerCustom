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

        var handler = this.serviceProvider.GetRequiredService<IQueryHandler<IQuery<TResult>, TResult>>();

        return await handler.Handle(query).ConfigureAwait(false);
    }
}