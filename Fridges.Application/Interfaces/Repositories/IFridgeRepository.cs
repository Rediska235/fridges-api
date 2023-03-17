using Fridges.Application.DTOs;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Interfaces.Repositories;

public interface IFridgeRepository
{
    IEnumerable<Fridge> GetFridges();
    Fridge GetFridgeById(Guid FridgeId);
    void InsertFridge(Fridge Fridge);
    void UpdateFridge(Fridge Fridge);
    void DeleteFridge(Guid FridgeId);
    void Save();
}
