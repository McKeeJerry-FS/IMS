using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

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

    public Task EditProductAsync(Product product)
    {
        if(_products.Any(x => x.ProductId == product.ProductId && x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
				return Task.CompletedTask;

            var inv = _products.FirstOrDefault(x => x.ProductId == product.ProductId);
			if (inv != null)
			{
				inv.ProductName = product.ProductName;
				inv.Quantity = product.Quantity;
				inv.Price = product.Price;
			}
			return Task.CompletedTask;
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_products);
			return _products.Where(x => x.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        var prod = _products.First(x => x.ProductId == productId);
            var newProd = new Product
            {
                ProductId = prod.ProductId,
                ProductName = prod.ProductName,
                Price = prod.Price,
                Quantity = prod.Quantity
            };

            return await Task.FromResult(newProd);
    }
}
