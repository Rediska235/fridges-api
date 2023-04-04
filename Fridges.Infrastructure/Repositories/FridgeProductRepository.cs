using Fridges.Application.DTOs;
using Fridges.Application.Repositories;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;
using Fridges.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fridges.Infrastructure.Repositories;

public class FridgeProductRepository : IFridgeProductRepository
{
    private readonly AppDbContext _db;

    public FridgeProductRepository(AppDbContext db)
    {
        _db = db;
    }

    public IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid fridgeId)
    {
        return _db.FridgeProducts
            .Include(fp => fp.Fridge)
            .Where(fp => fp.Fridge.Id == fridgeId)
            .Select(fp => new ProductQuantity
            {
                Product = fp.Product,
                Quantity = fp.Quantity
            });
    }

    public FridgeProduct GetFridgeProductByIds(Guid fridgeId, Guid productId)
    {
        var fridgeProduct = _db.FridgeProducts
            .Include(fp => fp.Fridge)
            .Include(fp => fp.Product)
            .FirstOrDefault(fp => fp.Fridge.Id == fridgeId && fp.Product.Id == productId);

        return fridgeProduct;
    }

    public void AddFridgeProduct(FridgeProduct fridgeProduct)
    {
        _db.Add(fridgeProduct);
    }

    public void UpdateProductQuantity(FridgeProduct fridgeProduct)
    {
        _db.Update(fridgeProduct);
    }

    public void RemoveFridgeProduct(RemoveProductsDto removeProductsDto)
    {
        var fridgeProduct = GetFridgeProductByIds(removeProductsDto.FridgeId, removeProductsDto.ProductId);
        if (fridgeProduct != null)
        {
            _db.Remove(fridgeProduct);
        }
    }

    public List<FridgeProduct> GetProductsWithZeroQuantity()
    {
        var ids = _db.FridgeProducts
            .FromSql($"SearchProductsWithZeroQuantity")
            .ToList()
            .Select(fp => fp.Id)
            .ToList();

        var fridgeProducts = _db.FridgeProducts
            .Include(fp => fp.Fridge)
            .Include(fp => fp.Product)
            .Where(fp => ids.Contains(fp.Id))
            .ToList();

        return fridgeProducts;
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
