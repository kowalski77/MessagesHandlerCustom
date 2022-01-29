using Microsoft.Extensions.DependencyInjection;

namespace MTrading;

internal sealed class CommandResultHandler<TCommand> : RequestHandlerBase<Result>
    where TCommand : ICommand
{
    public override async Task<Result> Handle(IRequest request, IServiceProvider serviceProvider)
    {
        var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        return await handler.Handle((TCommand)request).ConfigureAwait(false);
    }
}