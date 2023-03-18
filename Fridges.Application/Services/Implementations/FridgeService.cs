using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Application.Repositories;
using Fridges.Application.Services.Services;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;
using Fridges.Domain.Exceptions;

namespace Fridges.Application.Services.Implementations;

public class FridgeService : IFridgeService
{
    private readonly IFridgeRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IFridgeModelRepository _fridgeModelRepository;
    private readonly IFridgeProductRepository _fridgeProductRepository;

    public FridgeService(IFridgeRepository repository,
        IFridgeModelRepository fridgeModelRepository,
        IProductRepository productRepository,
        IFridgeProductRepository fridgeProductRepository)
    {
        _repository = repository;
        _fridgeModelRepository = fridgeModelRepository;
        _productRepository = productRepository;
        _fridgeProductRepository = fridgeProductRepository;
    }

    public IEnumerable<Fridge> GetAllFridges()
    {
        return _repository.GetFridges();
    }

    public Fridge GetFridgeById(Guid fridgeId)
    {
        return _repository.GetFridgeById(fridgeId);
    }

    public IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid fridgeId)
    {
        return _fridgeProductRepository.GetProductsByFridgeId(fridgeId);
    }

    public void AddProducts(Guid fridgeId, AddProductsDto addProductsDto)
    {
        FridgeProduct fridgeProduct = new()
        {
            Fridge = _repository.GetFridgeById(fridgeId),
            Product = _productRepository.GetProductById(addProductsDto.ProductId),
            Quantity = addProductsDto.Quanity
        };

        _fridgeProductRepository.AddProducts(fridgeProduct);
        _repository.Save();
    }

    public void RemoveProducts(Guid fridgeId, Guid productId)
    {
        RemoveProductsDto removeProductsDto = new()
        {
            FridgeId = fridgeId,
            ProductId = productId
        };

        _fridgeProductRepository.RemoveProducts(removeProductsDto);
        _repository.Save();
    }

    public Fridge CreateFridge(CreateFridgeDto createFridgeDto)
    {
        var fridgeName = createFridgeDto.Name.Trim();
        var ownerName = createFridgeDto.OwnerName?.Trim();
        
        if (AlreadyExists(fridgeName))
        {
            throw Exceptions.fridgeAlreadyExists;
        }

        var fridge = new Fridge()
        {
            Id = new Guid(),
            Name = fridgeName,
            OwnerName = ownerName,
            FridgeModel = _fridgeModelRepository.GetFridgeModelById(createFridgeDto.FridgeModelId)
        };
        _repository.InsertFridge(fridge);
        _repository.Save();

        return fridge;
    }

    public Fridge UpdateFridge(UpdateFridgeDto updateFridgeDto)
    {
        var fridgeName = updateFridgeDto.Name.Trim();
        var ownerName = updateFridgeDto.OwnerName?.Trim();
        
        var fridge = new Fridge()
        {
            Id = updateFridgeDto.Id,
            Name = fridgeName,
            OwnerName = ownerName,
            FridgeModel = _fridgeModelRepository.GetFridgeModelById(updateFridgeDto.FridgeModelId),
            FridgeProducts = null
        };

        _repository.UpdateFridge(fridge);
        _repository.Save();

        return fridge;
    }

    public void DeleteFridge(Guid fridgeId)
    {
        _repository.DeleteFridge(fridgeId);
        _repository.Save();
    }
    
    private bool AlreadyExists(string name)
    {
        try
        {
            _repository.GetFridgeByName(name);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
