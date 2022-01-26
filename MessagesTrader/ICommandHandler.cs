namespace MessagesTrader;

public interface ICommandHandler<in T> where T : ICommand
{
    Task<Result> Handle(T command);
}