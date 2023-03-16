using Fridges.API.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Interfaces.Services;

public interface IFridgeService
{
    IEnumerable<Fridge> GetAllFridges();
    Fridge GetFridgeById(Guid FridgeId);
    Fridge CreateFridge(FridgeCreateDto Fridge);
    void UpdateFridge(Fridge Fridge);
    void DeleteFridge(Guid FridgeID);
}
