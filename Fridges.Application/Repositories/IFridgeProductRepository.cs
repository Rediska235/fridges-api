using Fridges.Application.DTOs;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Repositories;

public interface IFridgeProductRepository
{
    IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid fridgeId);
    void AddProducts(FridgeProduct fridgeProduct);
    void RemoveProducts(RemoveProductsDto removeProductsDto);
    List<FridgeProduct> GetProductsWithZeroQuantity();
    void Save();
}
