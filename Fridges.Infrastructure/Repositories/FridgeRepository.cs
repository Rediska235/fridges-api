using Fridges.Application.DTOs;
using Fridges.Application.Interfaces.Repositories;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;
using Fridges.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fridges.Infrastructure.Repositories;

public class FridgeRepository : IFridgeRepository
{
    private readonly AppDbContext _db;

    public FridgeRepository(AppDbContext db)
    {
        _db = db;
    }

    public IEnumerable<Fridge> GetFridges()
    {
        return _db.Fridges.Include(f => f.FridgeModel);
    }

    public Fridge GetFridgeById(Guid FridgeId)
    {
        return _db.Fridges.FirstOrDefault(p => p.Id == FridgeId);
    }


    /// ////////////////////////////////////////////////////////////////////////////
    public IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid FridgeId)
    {
        return _db.FridgeProducts
            .Include(fp => fp.Fridge)
            .Where(fp => fp.Fridge.Id == FridgeId)
            .Select(fp => new ProductQuantity {
                Product = fp.Product, 
                Quanity = fp.Quantity
            });
    }
    /// ////////////////////////////////////////////////////////////////////////////

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

    public void InsertFridge(Fridge Fridge)
    {
        _db.Add(Fridge);
        Save();
    }

    public void UpdateFridge(Fridge Fridge)
    {
        _db.Update(Fridge);
        Save();
    }

    public void DeleteFridge(Guid FridgeId)
    {
        var fridge = GetFridgeById(FridgeId);
        _db.Remove(fridge);
        Save();
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
