﻿using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Services.Services;

public interface IFridgeService
{
    IEnumerable<Fridge> GetAllFridges();
    Fridge GetFridgeById(Guid FridgeId);
    IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid FridgeId);
    void AddProducts(Guid fridgeId, AddProductsDto addProductsDto);
    void RemoveProducts(Guid FridgeId, Guid ProductId);
    Fridge CreateFridge(CreateFridgeDto Fridge);
    Fridge UpdateFridge(UpdateFridgeDto updateFridgeDto);
    void DeleteFridge(Guid FridgeID);
}