using IMS.CoreBusiness;
using IMS.UseCases;

namespace IMS.Plugins.InMemory;

public class ProductRepository : IProductRepository
{

    private List<Product> _products;

    public ProductRepository(){
        _products = new List<Product>() 
			{ 
				new Product { ProductId = 1, ProductName = "Bike", Quantity = 10, Price = 150},
				new Product { ProductId = 2, ProductName = "Car", Quantity = 10, Price = 25000},
				
			};
    }

    public Task AddProductAsync(Product product)
    {
        if (_products.Any(x => x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase))){
				return Task.CompletedTask;
			}
            //find the maximum product Id
			var maxId = _products.Max(x => x.ProductId);
            // assign new product the new product id
			product.ProductId = maxId + 1;

			_products.Add(product);
			return Task.CompletedTask;
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_products);
			return _products.Where(x => x.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase));
    }
}
