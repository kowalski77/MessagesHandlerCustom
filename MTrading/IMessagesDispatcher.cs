namespace MTrading;

public interface IMessagesDispatcher
{
    Task<Result> DispatchAsync<TCommand>(TCommand command)
        where TCommand : ICommand;
}