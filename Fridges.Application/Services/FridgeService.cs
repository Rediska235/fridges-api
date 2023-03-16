using Fridges.API.DTOs;
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

    public Fridge CreateFridge(FridgeCreateDto Fridge)
    {
        var fridge = new Fridge()
        {
            Id = new Guid(),
            Name = Fridge.Name,
            OwnerName = Fridge.OwnerName
            //FridgeModel =                                  //возможно достать FridgeModel из DB по Guid
        };
        _repository.InsertFridge(fridge);

        return fridge;
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
