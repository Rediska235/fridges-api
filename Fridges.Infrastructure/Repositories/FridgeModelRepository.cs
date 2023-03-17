using Fridges.Application.Interfaces.Repositories;
using Fridges.Domain.Entities;
using Fridges.Infrastructure.Data;

namespace Fridges.Infrastructure.Repositories;

public class FridgeModelRepository : IFridgeModelRepository
{
    private readonly AppDbContext _db;

    public FridgeModelRepository(AppDbContext db)
    {
        _db = db;
    }

    public FridgeModel GetFridgeModelById(Guid Id)
    {
        return _db.FridgeModels.FirstOrDefault(fm => fm.Id == Id);
    }
}
