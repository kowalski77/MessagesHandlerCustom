using BenchmarkDotNet.Attributes;
using MediatR;
using MessagesTrader.PerformanceTests.Handlers;
using MessagesTrader.PerformanceTests.Support;
using Microsoft.Extensions.DependencyInjection;
using MTrading;

namespace MessagesTrader.PerformanceTests;

[MemoryDiagnoser]
public class HandlerBenchmark
{
    private readonly CustomTestCommand customTestCommand = new()
    {
        Id = Guid.NewGuid(),
        Name = "New product name"
    };

    private readonly MediatorTestCommand testCommand = new()
    {
        Id = Guid.NewGuid(),
        Name = "New product name"
    };

    private IServiceProvider? serviceProvider;

    public HandlerBenchmark()
    {
        Console.WriteLine("Configure services...");
        this.ConfigureServices();
    }

    [Benchmark]
    public async Task<Result> MediatRNuget()
    {
        using var scope = this.serviceProvider!.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var result = await mediator.Send(this.testCommand);

        return result;
    }

    [Benchmark]
    public async Task<Result> CustomDispatcher()
    {
        using var scope = this.serviceProvider!.CreateScope();
        var messagesDispatcher = scope.ServiceProvider.GetRequiredService<IMessagesDispatcher>();

        var result = await messagesDispatcher.ExecuteAsync(this.customTestCommand);

        return result;
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        Console.WriteLine("Disposing services...");
        switch (this.serviceProvider)
        {
            case null:
                return;
            case IDisposable disposable:
                disposable.Dispose();
                break;
        }
    }

    private void ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddScoped<IProductRepository, ProductRepository>();

        // MediatR
        services.AddScoped<IMediator, Mediator>();
        _ = services.AddTransient<ServiceFactory>(p => p.GetService!);
        services.AddScoped<IRequestHandler<MediatorTestCommand, Result>, MediatorTestCommandHandler>();

        // Custom Dispatcher
        services.AddMessagesDispatcherFromAssembly<MediatorTestCommandHandler>();

        this.serviceProvider = services.BuildServiceProvider(true);
    }
}