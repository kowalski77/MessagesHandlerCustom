using MessagesHandlerCustom.PerformanceTests.Support;
using MessagesHandlerCustom.Utils;

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
        var product = await productRepository.GetAsync(command.Id);
        if (product is null)
        {
            return Result.Fail("whatever...");
        }

        product.Name = command.Name;

        return Result.Ok();
    }
}
