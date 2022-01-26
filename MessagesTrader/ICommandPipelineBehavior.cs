namespace MessagesTrader;

public interface ICommandPipelineBehavior<in T> where T : ICommand
{
    Task<Result> Handle(T command, CommandPipelineHandler nextHandler);
}