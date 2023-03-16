using Fridges.Domain.Entities;

namespace Fridges.Application.Interfaces.Services;

public interface IFridgeService
{
    IEnumerable<Fridge> GetAllFridges();
    Fridge GetFridgeById(Guid FridgeId);
    void CreateFridge(Fridge Fridge);
    void UpdateFridge(Fridge Fridge);
    void DeleteFridge(Guid FridgeID);
}
