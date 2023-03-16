using Fridges.Application.Interfaces.Repositories;
using Fridges.Application.Interfaces.Services;
using Fridges.Domain.Entities;

namespace Fridges.Application.Implementations;

public class FridgeService : IFridgeService
{
    private readonly IFridgeRepository _repository;

    public FridgeService(IFridgeRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Fridge> GetAllFridges()
    {
        return _repository.GetFridges();
    }

    public Fridge GetFridgeById(Guid FridgeId)
    {
        return _repository.GetFridgeById(FridgeId);
    }

    public void CreateFridge(Fridge Fridge)
    {
        _repository.InsertFridge(Fridge);
    }

    public void UpdateFridge(Fridge Fridge)
    {
        _repository.UpdateFridge(Fridge);
    }

    public void DeleteFridge(Guid FridgeId)
    {
        _repository.DeleteFridge(FridgeId);
    }
}
