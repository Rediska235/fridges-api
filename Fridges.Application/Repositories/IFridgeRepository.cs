using Fridges.Domain.Entities;

namespace Fridges.Application.Repositories;

public interface IFridgeRepository
{
    IEnumerable<Fridge> GetFridges();
    Fridge GetFridgeById(Guid fridgeId);
    Fridge GetFridgeByName(string fridgeName);
    void InsertFridge(Fridge fridge);
    void UpdateFridge(Fridge fridge);
    void DeleteFridge(Guid fridgeId);
    void Save();
}
