namespace MTrading;

public interface IMessageTrader
{
    Task<Result> ExecuteAsync<TCommand>(TCommand command)
        where TCommand : ICommand;

    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
}