using IMS.CoreBusiness;

namespace IMS.UseCases.Products;

public interface IAddProductUseCase
{
    public Task ExecuteAsync(Product product);
}
