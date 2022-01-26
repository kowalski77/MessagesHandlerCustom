
namespace MessagesTrader.PerformanceTests.Support;

public interface IProductRepository
{
    ValueTask<Product> GetAsync(Guid id);
}
