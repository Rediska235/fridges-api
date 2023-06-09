﻿using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Services.Interfaces;

public interface IFridgeService
{
    IEnumerable<Fridge> GetAllFridges();
    Fridge GetFridgeById(Guid fridgeId);
    IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid fridgeId);
    void AddProducts(Guid fridgeId, AddProductsDto addProductsDto);
    void RemoveProducts(Guid fridgeId, Guid productId);
    void UpdateProductsQuantity();
    Fridge CreateFridge(CreateFridgeDto fridge);
    Fridge UpdateFridge(UpdateFridgeDto updateFridgeDto);
    void DeleteFridge(Guid fridgeID);
}
