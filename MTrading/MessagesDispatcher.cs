using System.Collections.Concurrent;

namespace MTrading;

internal sealed class MessagesDispatcher : IMessagesDispatcher
{
    private static readonly ConcurrentDictionary<Type, RequestHandlerBase> RequestHandlers = new();
    private readonly IServiceProvider serviceProvider;

    public MessagesDispatcher(IServiceProvider serviceProvider)
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
        var requestHandler = (RequestHandler<Result>)RequestHandlers.GetOrAdd(commandType,
            static t => (RequestHandlerBase)Activator.CreateInstance(typeof(CommandHandler<>).MakeGenericType(t))!);

        var result = await requestHandler.Handle(command, this.serviceProvider).ConfigureAwait(false);

        return result;
    }

    private async Task<TResult> QueryInternalAsync<TResult>(IQuery<TResult> query)
    {
        var queryType = query.GetType();
        var requestHandler = (RequestHandler<TResult>)RequestHandlers.GetOrAdd(queryType,
            static t => (RequestHandlerBase)Activator.CreateInstance(typeof(QueryHandler<,>).MakeGenericType(t, typeof(TResult)))!);

        var result = await requestHandler.Handle(query, this.serviceProvider).ConfigureAwait(false);

        return result;
    }
}