using Fridges.Application.DTOs;
using Fridges.Application.Repositories;
using Fridges.Application.Services.Interfaces;
using Fridges.Domain.Entities;
using Fridges.Domain.Exceptions;

namespace Fridges.Application.Services.Implementations;

public class FridgeModelService : IFridgeModelService
{
    private readonly IFridgeModelRepository _repository;

    public FridgeModelService(IFridgeModelRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<FridgeModel> GetAllFridgeModels()
    {
        return _repository.GetFridgeModels();
    }

    public FridgeModel GetFridgeModelById(Guid fridgeModelId)
    {
        return _repository.GetFridgeModelById(fridgeModelId);
    }

    public FridgeModel CreateFridgeModel(CreateFridgeModelDto createFridgeModelDto)
    {
        var fridgeModelName = createFridgeModelDto.Name.Trim();
        if (AlreadyExists(fridgeModelName))
        {
            throw Exceptions.fridgeModelAlreadyExists;
        }

        var fridgeModel = new FridgeModel()
        {
            Id = Guid.NewGuid(),
            Name = fridgeModelName,
            Year = createFridgeModelDto.Year
        };

        _repository.InsertFridgeModel(fridgeModel);
        _repository.Save();

        return fridgeModel;
    }

    public FridgeModel UpdateFridgeModel(UpdateFridgeModelDto updateFridgeModelDto)
    {
        var fridgeModel = _repository.GetFridgeModelById(updateFridgeModelDto.Id);

        var fridgeModelName = updateFridgeModelDto.Name.Trim();
        if (fridgeModelName != fridgeModel.Name && AlreadyExists(fridgeModelName))
        {
            throw Exceptions.fridgeModelAlreadyExists;
        }

        fridgeModel.Name = fridgeModelName;
        fridgeModel.Year = updateFridgeModelDto.Year;

        _repository.UpdateFridgeModel(fridgeModel);
        _repository.Save();

        return fridgeModel;
    }

    public void DeleteFridgeModel(Guid fridgeModelId)
    {
        _repository.DeleteFridgeModel(fridgeModelId);
        _repository.Save();
    }

    private bool AlreadyExists(string name)
    {
        try
        {
            _repository.GetFridgeModelByName(name);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
