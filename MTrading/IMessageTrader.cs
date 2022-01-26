namespace MTrading;

public interface IMessageTrader
{
    Task<Result> DispatchAsync<TCommand>(TCommand command)
        where TCommand : ICommand;
}