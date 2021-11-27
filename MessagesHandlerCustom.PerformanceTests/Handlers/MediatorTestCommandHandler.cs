using MediatR;
using MessagesHandlerCustom.PerformanceTests.Support;
using MessagesHandlerCustom.Utils;

namespace MessagesHandlerCustom.PerformanceTests.Handlers;

public class MediatorTestCommandHandler : IRequestHandler<MediatorTestCommand, Result>
{
    private readonly IProductRepository productRepository;

    public MediatorTestCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Result> Handle(MediatorTestCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetAsync(request.Id);
        if (product is null)
        {
            return Result.Fail("whatever...");
        }

        product.Name = request.Name;

        return Result.Ok();
    }
}
