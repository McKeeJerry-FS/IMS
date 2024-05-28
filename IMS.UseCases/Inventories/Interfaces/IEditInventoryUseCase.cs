using IMS.CoreBusiness;

namespace IMS.UseCases;

public interface IEditInventoryUseCase
{
    public Task ExecuteAsync(Inventory inventory);
}
