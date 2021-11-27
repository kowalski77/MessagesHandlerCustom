using MediatR;
using MessagesHandlerCustom.Utils;

namespace MessagesHandlerCustom.PerformanceTests.Handlers;

public class MediatorTestCommand : IRequest<Result>
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;
}
