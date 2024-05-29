using IMS.CoreBusiness;

namespace IMS.UseCases.Products;

public interface IViewProductsByNameUseCase
{
    public Task<IEnumerable<Product>> ExecuteAsync(string name = "");
}
