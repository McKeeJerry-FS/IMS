using IMS.CoreBusiness;

namespace IMS.UseCases;

public interface IProductRepository
{
    
    Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
}
