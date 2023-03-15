using Fridges.Domain.Entities;

namespace Fridges.Application.Interfaces.Repositories;

public interface IFridgeRepository
{
    IEnumerable<Fridge> GetFridges();
    Fridge GetFridgeByID(Guid FridgeId);
    void InsertFridge(Fridge Fridge);
    void UpdateFridge(Fridge Fridge);
    void DeleteFridge(Guid FridgeID);
    void Save();
}
