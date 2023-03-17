using Fridges.Application.DTOs;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Interfaces.Repositories;

public interface IFridgeProductRepository
{
    IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid FridgeId);
    void AddProducts(FridgeProduct fridgeProduct);
    void RemoveProducts(RemoveProductsDto removeProductsDto);
    void Save();
}
