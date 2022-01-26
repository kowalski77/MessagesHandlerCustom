using MessagesHandlerCustom.PerformanceTests.Support;
using MessagesTrader;

namespace MessagesHandlerCustom.PerformanceTests.Handlers;

public class CustomTestCommandHandler : ICommandHandler<CustomTestCommand>
{
    private readonly IProductRepository productRepository;

    public CustomTestCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Result> Handle(CustomTestCommand command)
    {
        var product = await this.productRepository.GetAsync(command.Id);
        product.Name = command.Name;

        return Result.Ok();
    }
}