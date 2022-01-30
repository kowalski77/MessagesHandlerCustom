using MTrading;

namespace MessagesTrader.Application;

public class DecoratorCommandBehavior<TCommand> : ICommandPipelineBehavior<TCommand>
    where TCommand : ICommand
{
    public async Task<Result> Handle(TCommand command, CommandPipelineHandler nextHandler)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(nextHandler);

        Console.WriteLine("Before decorated command");

        var result = await nextHandler().ConfigureAwait(false);

        Console.WriteLine("After decorated command");

        return result;
    }
}