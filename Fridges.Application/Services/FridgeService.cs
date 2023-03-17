using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Application.Interfaces.Repositories;
using Fridges.Application.Interfaces.Services;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Implementations;

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

    public Fridge GetFridgeById(Guid FridgeId)
    {
        return _repository.GetFridgeById(FridgeId);
    }

    public IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid FridgeId)
    {
        return _fridgeProductRepository.GetProductsByFridgeId(FridgeId);
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
    }

    public void RemoveProducts(Guid FridgeId, Guid ProductId)
    {
        RemoveProductsDto removeProductsDto = new()
        {
            FridgeId = FridgeId,
            ProductId = ProductId
        };

        _fridgeProductRepository.RemoveProducts(removeProductsDto);
    }

    public Fridge CreateFridge(CreateFridgeDto Fridge)
    {
        var fridge = new Fridge()
        {
            Id = new Guid(),
            Name = Fridge.Name,
            OwnerName = Fridge.OwnerName,
            FridgeModel = _fridgeModelRepository.GetFridgeModelById(Fridge.FridgeModelId)
        };
        _repository.InsertFridge(fridge);

        return fridge;
    }

    public Fridge UpdateFridge(UpdateFridgeDto updateFridgeDto)
    {
        var fridge = new Fridge()
        {
            Id = updateFridgeDto.Id,
            Name = updateFridgeDto.Name,
            OwnerName = updateFridgeDto.OwnerName,
            FridgeModel = _fridgeModelRepository.GetFridgeModelById(updateFridgeDto.FridgeModelId),
            FridgeProducts = null
        };

        _repository.UpdateFridge(fridge);

        return _repository.GetFridgeById(fridge.Id);
    }

    public void DeleteFridge(Guid FridgeId)
    {
        _repository.DeleteFridge(FridgeId);
    }
}
