﻿using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Interfaces.Services;

public interface IFridgeService
{
    IEnumerable<Fridge> GetAllFridges();
    Fridge GetFridgeById(Guid FridgeId);
    IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid FridgeId);
    void AddProducts(AddProductsDto addProductsDto);
    void RemoveProducts(Guid FridgeId, Guid ProductId);
    Fridge CreateFridge(FridgeCreateDto Fridge);
    void UpdateFridge(Fridge Fridge);
    void DeleteFridge(Guid FridgeID);
}
