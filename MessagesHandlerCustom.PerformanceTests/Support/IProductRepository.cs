
namespace MessagesHandlerCustom.PerformanceTests.Support;

public interface IProductRepository
{
    Task<Product?> GetAsync(Guid id);
}
