namespace MessagesHandlerCustom.PerformanceTests.Support;

public class ProductRepository : IProductRepository
{
    public ValueTask<Product> GetAsync(Guid id)
    {
        return new ValueTask<Product>(new Product
        {
            Id = id,
            Name = "Product 1"
        });
    }
}
