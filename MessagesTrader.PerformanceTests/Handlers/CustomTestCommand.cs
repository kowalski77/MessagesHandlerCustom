using MTrading;

namespace MessagesTrader.PerformanceTests.Handlers;

public class CustomTestCommand : ICommand
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;
}
