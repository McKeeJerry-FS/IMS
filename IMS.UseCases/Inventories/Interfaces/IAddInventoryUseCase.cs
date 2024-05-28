using IMS.CoreBusiness;

namespace IMS.UseCases.Inventories;

public interface IAddInventoryUseCase
{
    public Task ExecuteAsync(Inventory inventory);
}
