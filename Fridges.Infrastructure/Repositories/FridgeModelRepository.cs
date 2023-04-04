using Fridges.Application.Repositories;
using Fridges.Domain.Entities;
using Fridges.Domain.Exceptions;
using Fridges.Infrastructure.Data;

namespace Fridges.Infrastructure.Repositories;

public class FridgeModelRepository : IFridgeModelRepository
{
    private readonly AppDbContext _db;

    public FridgeModelRepository(AppDbContext db)
    {
        _db = db;
    }

    public IEnumerable<FridgeModel> GetFridgeModels()
    {
        return _db.FridgeModels;
    }

    public FridgeModel GetFridgeModelById(Guid fridgeModelId)
    {
        var fridgeModel = _db.FridgeModels.FirstOrDefault(p => p.Id == fridgeModelId);
        if (fridgeModel == null)
        {
            throw Exceptions.fridgeModelNotFound;
        }

        return fridgeModel;
    }

    public FridgeModel GetFridgeModelByName(string name)
    {
        var fridgeModel = _db.FridgeModels.FirstOrDefault(p => p.Name == name);
        if (fridgeModel == null)
        {
            throw Exceptions.fridgeModelNotFound;
        }

        return fridgeModel;
    }

    public void InsertFridgeModel(FridgeModel fridgeModel)
    {
        _db.Add(fridgeModel);
    }

    public void UpdateFridgeModel(FridgeModel fridgeModel)
    {
        _db.Update(fridgeModel);
    }

    public void DeleteFridgeModel(Guid fridgeModelId)
    {
        var fridgeModel = GetFridgeModelById(fridgeModelId);
        _db.Remove(fridgeModel);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
