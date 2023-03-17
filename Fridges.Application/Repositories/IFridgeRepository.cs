﻿using Fridges.Domain.Entities;

namespace Fridges.Application.Repositories;

public interface IFridgeRepository
{
    IEnumerable<Fridge> GetFridges();
    Fridge GetFridgeById(Guid FridgeId);
    void InsertFridge(Fridge Fridge);
    void UpdateFridge(Fridge Fridge);
    void DeleteFridge(Guid FridgeId);
    void Save();
}