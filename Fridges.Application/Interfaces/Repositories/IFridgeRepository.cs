using Fridges.Application.DTOs;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Interfaces.Repositories;

public interface IFridgeRepository
{
    IEnumerable<Fridge> GetFridges();
    Fridge GetFridgeById(Guid FridgeId);
    IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid FridgeId);
    void AddProducts(FridgeProduct fridgeProduct);
    void RemoveProducts(RemoveProductsDto removeProductsDto);
    void InsertFridge(Fridge Fridge);
    void UpdateFridge(Fridge Fridge);
    void DeleteFridge(Guid FridgeId);
    void Save();
}
