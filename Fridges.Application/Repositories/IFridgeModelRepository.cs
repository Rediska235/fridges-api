using Fridges.Domain.Entities;

namespace Fridges.Application.Repositories;

public interface IFridgeModelRepository
{
    IEnumerable<FridgeModel> GetFridgeModels();
    FridgeModel GetFridgeModelById(Guid fridgeModelId);
    FridgeModel GetFridgeModelByName(string fridgeModelName);
    void InsertFridgeModel(FridgeModel fridgeModel);
    void UpdateFridgeModel(FridgeModel fridgeModel);
    void DeleteFridgeModel(Guid fridgeModelId);
    void Save();
}
