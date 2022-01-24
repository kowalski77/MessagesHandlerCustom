using Microsoft.Extensions.DependencyInjection;

namespace MessagesHandlerCustom.Utils;

public class MessagesDispatcher
{
    private readonly IServiceProvider serviceProvider;

    public MessagesDispatcher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public async Task<Result> DispatchAsync<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        ArgumentNullException.ThrowIfNull(command);

        var handler = this.serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        var result = await handler.Handle(command).ConfigureAwait(false);

        return result;
    }
}