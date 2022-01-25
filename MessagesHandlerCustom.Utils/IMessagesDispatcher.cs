namespace MessagesHandlerCustom.Utils;

public interface IMessagesDispatcher
{
    Task<Result> DispatchAsync<TCommand>(TCommand command)
        where TCommand : ICommand;
}