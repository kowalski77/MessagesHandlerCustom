namespace MessagesHandlerCustom.PerformanceTests.Support;

public class ProductRepository : IProductRepository
{
    public Task<Product?> GetAsync(Guid id)
    {
        return Task.FromResult<Product?>(new Product
        {
            Id = id,
            Name = "Product 1"
        });
    }
}
