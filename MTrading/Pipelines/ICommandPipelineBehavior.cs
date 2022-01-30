namespace MTrading;

public interface ICommandPipelineBehavior<in TCommand> 
    where TCommand : ICommand
{
    Task<Result> Handle(TCommand command, CommandPipelineHandler nextHandler);
}