using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Inventories;

public interface IEditInventoryUseCase
{
    public Task ExecuteAsync(Inventory inventory);
}
