using Fridges.Application.Interfaces.Repositories;
using Fridges.Domain.Entities;
using Fridges.Infrastructure.Data;

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
        return _db.Fridges;
    }

    public Fridge GetFridgeById(Guid FridgeId)
    {
        return _db.Fridges.FirstOrDefault(p => p.Id == FridgeId);
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
