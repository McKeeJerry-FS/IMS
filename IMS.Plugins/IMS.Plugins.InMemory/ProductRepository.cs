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

    public Task UpdateProductAsync(Product product)
    {
        if (_products.Any(x => x.ProductId != product.ProductId && x.ProductName.ToLower() == product.ProductName.ToLower())) return Task.CompletedTask; 

        var prod = _products.FirstOrDefault(x => x.ProductId == product.ProductId);
        if (prod != null) 
        { 
            prod.ProductName = product.ProductName;
            prod.Quantity = product.Quantity;
            prod.Price = product.Price;
            prod.ProductInventories = prod.ProductInventories;
        }
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_products);
			return _products.Where(x => x.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        var prod = _products.FirstOrDefault(x => x.ProductId == productId);
        var newProd = new Product();
        if (prod is not null)    
        {
            newProd.ProductId = prod.ProductId;
            newProd.ProductName = prod.ProductName;
            newProd.Price = prod.Price;
            newProd.Quantity = prod.Quantity;
            newProd.ProductInventories = new List<ProductInventory>();
            if(prod.ProductInventories != null && prod.ProductInventories.Count > 0)
            {
                foreach(var prodInv in prod.ProductInventories)
                {
                    var newProdInv = new ProductInventory
                    {
                        InventoryId = prodInv.InventoryId,
                        ProductId = prodInv.ProductId,
                        Product = prod,
                        Inventory = new Inventory(),
                        InventoryQuantity = prodInv.InventoryQuantity,
                    };
                    if(prodInv.Inventory is not null)
                    {
                        newProdInv.Inventory.InventoryId = prodInv.Inventory.InventoryId;
                        newProdInv.Inventory.InventoryName = prodInv.Inventory.InventoryName;
                        newProdInv.Inventory.Price = prodInv.Inventory.Price;
                        newProdInv.Inventory.Quantity = prodInv.Inventory.Quantity;
                    }
                    newProd.ProductInventories.Add(newProdInv);
                }
            } 
        };

        return await Task.FromResult(newProd);
    }
}
