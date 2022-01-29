using System.Collections.Concurrent;

namespace MTrading;

public class MessageTrader : IMessageTrader
{
    private static readonly ConcurrentDictionary<Type, RequestHandlerBase> RequestHandlers = new();
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
        var requestHandler = (RequestHandlerWrapper<Result>)RequestHandlers.GetOrAdd(commandType,
            static t => (RequestHandlerBase)Activator.CreateInstance(typeof(CommandResultHandler<>).MakeGenericType(t))!);

        var result = await requestHandler.Handle(command, this.serviceProvider).ConfigureAwait(false);

        return result;
    }

    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        ArgumentNullException.ThrowIfNull(query);

        var queryType = query.GetType();
        var requestHandler = (RequestHandlerWrapper<TResult>)RequestHandlers.GetOrAdd(queryType,
            static t => (RequestHandlerBase)Activator.CreateInstance(typeof(QueryResultHandler<,>).MakeGenericType(t, typeof(TResult)))!);

        var result = await requestHandler.Handle(query, this.serviceProvider).ConfigureAwait(false);

        return result;
    }
}