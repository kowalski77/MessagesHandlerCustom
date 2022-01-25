namespace MessagesHandlerCustom.Utils;

public class MessagesDispatcher : IMessagesDispatcher
{
    private readonly IServiceProvider serviceProvider;

    public MessagesDispatcher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public Task<Result> DispatchAsync<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        ArgumentNullException.ThrowIfNull(command);

        return CommandHandlerCore.Handle(command, this.serviceProvider);
    }
}