using Fridges.Application.DTOs;
using Fridges.Application.Repositories;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;
using Fridges.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
                Quanity = fp.Quantity
            });
    }

    public void AddProducts(FridgeProduct fridgeProduct)
    {
        _db.Add(fridgeProduct);
        Save();
    }

    public void RemoveProducts(RemoveProductsDto removeProductsDto)
    {
        var fridgeProduct = _db.FridgeProducts
            .FirstOrDefault(fp => fp.Product.Id == removeProductsDto.ProductId
                               && fp.Fridge.Id == removeProductsDto.FridgeId);

        _db.FridgeProducts.Remove(fridgeProduct);
        Save();
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
