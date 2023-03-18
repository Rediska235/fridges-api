using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Services.Interfaces;

public interface IFridgeModelService
{
    IEnumerable<FridgeModel> GetAllFridgeModels();
    FridgeModel GetFridgeModelById(Guid fridgeModelId);
    FridgeModel CreateFridgeModel(CreateFridgeModelDto fridgeModel);
    FridgeModel UpdateFridgeModel(UpdateFridgeModelDto updatefridgeModelDto);
    void DeleteFridgeModel(Guid fridgeModelID);
}
