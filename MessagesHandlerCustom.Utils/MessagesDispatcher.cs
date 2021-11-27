using Microsoft.Extensions.DependencyInjection;

namespace MessagesHandlerCustom.Utils;

public class MessagesDispatcher
{
    private readonly IServiceProvider serviceProvider;

    public MessagesDispatcher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public async Task<Result> DispatchAsync(ICommand command)
    {
        var commandHandlerType = typeof(ICommandHandler<>);
        var commandType = command.GetType();

        var handlerType = commandHandlerType.MakeGenericType(commandType);
        
        dynamic handler = serviceProvider.GetRequiredService(handlerType);
        var result = await handler.Handle((dynamic)command);

        return result;
    }
}