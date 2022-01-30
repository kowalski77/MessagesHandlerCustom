namespace MTrading;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<Result> Handle(TCommand command);
}