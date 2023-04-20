using Fridges.Application.DTOs;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Repositories;

public interface IFridgeProductRepository
{
    IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid fridgeId);
    void AddFridgeProduct(FridgeProduct fridgeProduct);
    void RemoveFridgeProduct(RemoveProductsDto removeProductsDto);
    void UpdateProductQuantity(FridgeProduct fridgeProduct);
    List<FridgeProduct> GetProductsWithZeroQuantity();
    FridgeProduct GetFridgeProductByIds(Guid fridgeId, Guid productId);
    void Save();
}
