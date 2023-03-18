using Fridges.Application.Repositories;
using Fridges.Domain.Entities;
using Fridges.Domain.Exceptions;
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
        var fridge = _db.Fridges
            .Include(f => f.FridgeModel)
            .FirstOrDefault(f => f.Id == FridgeId);

        if (fridge == null)
        {
            throw Exceptions.fridgeNotFound;
        }

        return fridge;
    }

    public Fridge GetFridgeByName(string Name)
    {
        var fridge = _db.Fridges.FirstOrDefault(f => f.Name == Name);

        if (fridge == null)
        {
            throw Exceptions.fridgeNotFound;
        }

        return fridge;
    }

    public void InsertFridge(Fridge Fridge)
    {
        _db.Add(Fridge);
    }

    public void UpdateFridge(Fridge Fridge)
    {
        _db.Update(Fridge);
    }

    public void DeleteFridge(Guid FridgeId)
    {
        var fridge = GetFridgeById(FridgeId);
        _db.Remove(fridge);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
