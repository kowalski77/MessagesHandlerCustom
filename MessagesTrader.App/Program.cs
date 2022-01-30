using MessagesTrader.App;
using MessagesTrader.Application;
using MessagesTrader.Domain;
using MessagesTrader.Infrastructure;
using MTrading;

IServiceProvider? serviceProvider;

ConfigureServices();

Console.WriteLine("Press any key to start...");
Console.ReadKey();

using (var scope = serviceProvider!.CreateScope())
{
    var startup = scope.ServiceProvider.GetRequiredService<Endpoints>();

    var enrollStudentDto = new EnrollStudentDto
    {
        CourseId = Guid.NewGuid(),
        StudentId = Guid.NewGuid()
    };

    Console.WriteLine("Command");
    await startup.EnrollStudentAsync(enrollStudentDto);

    Console.WriteLine("Query");
    await startup.GetStudentById(Guid.NewGuid());
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

DisposeServices();

void ConfigureServices()
{
    var services = new ServiceCollection();

    services.AddMessagesDispatcherFromAssembly<EnrollStudentCommandHandler>();
    services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(DecoratorCommandBehavior<>));
    services.AddScoped(typeof(IQueryPipelineBehavior<,>), typeof(DecoratorQueryBehavior<,>));

    services.AddScoped<IStudentRepository, StudentRepository>();
    services.AddScoped<ICourseRepository, CourseRepository>();
    services.AddScoped<Endpoints>();

    serviceProvider = services.BuildServiceProvider(true);
}

void DisposeServices()
{
    switch (serviceProvider)
    {
        case null:
            return;
        case IDisposable disposable:
            disposable.Dispose();
            break;
    }
}