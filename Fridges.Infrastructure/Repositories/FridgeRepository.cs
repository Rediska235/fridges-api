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

    public Fridge GetFridgeById(Guid fridgeId)
    {
        var fridge = _db.Fridges
            .Include(f => f.FridgeModel)
            .FirstOrDefault(f => f.Id == fridgeId);

        if (fridge == null)
        {
            throw Exceptions.fridgeNotFound;
        }

        return fridge;
    }

    public Fridge GetFridgeByName(string name)
    {
        var fridge = _db.Fridges.FirstOrDefault(f => f.Name == name);

        if (fridge == null)
        {
            throw Exceptions.fridgeNotFound;
        }

        return fridge;
    }

    public void InsertFridge(Fridge fridge)
    {
        _db.Add(fridge);
    }

    public void UpdateFridge(Fridge fridge)
    {
        _db.Update(fridge);
    }

    public void DeleteFridge(Guid fridgeId)
    {
        var fridge = GetFridgeById(fridgeId);
        _db.Remove(fridge);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
