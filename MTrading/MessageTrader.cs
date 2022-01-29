namespace MTrading;

public class MessageTrader : IMessageTrader
{
    private readonly IServiceProvider serviceProvider;

    public MessageTrader(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public async Task<Result> ExecuteAsync<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        ArgumentNullException.ThrowIfNull(command);

        var commandType = command.GetType();
        var requestHandler = (RequestHandlerBase<Result>)Activator.CreateInstance(typeof(CommandResultHandler<>)
            .MakeGenericType(commandType))!;

        var result = await requestHandler.Handle(command, this.serviceProvider).ConfigureAwait(false);

        return result;
    }

    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        ArgumentNullException.ThrowIfNull(query);

        var queryType = query.GetType();
        var requestHandler = (RequestHandlerBase<TResult>)Activator.CreateInstance(typeof(QueryResultHandler<,>)
            .MakeGenericType(queryType, typeof(TResult)))!;

        var result = await requestHandler.Handle(query, this.serviceProvider).ConfigureAwait(false);

        return result;
    }
}