﻿using Fridges.Application.Repositories;
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

    public FridgeModel GetFridgeModelById(Guid id)
    {
        return _db.FridgeModels.FirstOrDefault(fm => fm.Id == id);
    }
}
