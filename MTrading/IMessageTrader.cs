namespace MTrading;

public interface IMessageTrader
{
    Task<Result> SendAsync<TCommand>(TCommand command)
        where TCommand : ICommand;

    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
}