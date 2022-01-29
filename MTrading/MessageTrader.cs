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

    public Task<Result> ExecuteAsync<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        ArgumentNullException.ThrowIfNull(command);

        return this.ExecuteInternalAsync(command);
    }

    public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        ArgumentNullException.ThrowIfNull(query);

        return this.QueryInternalAsync(query);
    }

    private async Task<Result> ExecuteInternalAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        var commandType = command.GetType();
        var requestHandler = (RequestHandlerWrapper<Result>)RequestHandlers.GetOrAdd(commandType,
            static t => (RequestHandlerBase)Activator.CreateInstance(typeof(CommandResultHandler<>).MakeGenericType(t))!);

        var result = await requestHandler.Handle(command, this.serviceProvider).ConfigureAwait(false);

        return result;
    }

    private async Task<TResult> QueryInternalAsync<TResult>(IQuery<TResult> query)
    {
        var queryType = query.GetType();
        var requestHandler = (RequestHandlerWrapper<TResult>)RequestHandlers.GetOrAdd(queryType,
            static t => (RequestHandlerBase)Activator.CreateInstance(typeof(QueryResultHandler<,>).MakeGenericType(t, typeof(TResult)))!);

        var result = await requestHandler.Handle(query, this.serviceProvider).ConfigureAwait(false);

        return result;
    }
}