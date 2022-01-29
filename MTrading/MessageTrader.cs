namespace MTrading;

public class MessageTrader : IMessageTrader
{
    private readonly IServiceProvider serviceProvider;

    public MessageTrader(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public Task<Result> ExecuteAsync<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        ArgumentNullException.ThrowIfNull(command);

        return CommandHandlerCore.Handle(command, this.serviceProvider);
    }

    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        ArgumentNullException.ThrowIfNull(query);

        var queryType = query.GetType();
        var queryRequestHandler = (RequestHandlerBase<TResult>)Activator.CreateInstance(typeof(QueryResultHandler<,>)
            .MakeGenericType(queryType, typeof(TResult)))!;
        var result = await queryRequestHandler.Handle(query, this.serviceProvider).ConfigureAwait(false);

        return result;
    }
}