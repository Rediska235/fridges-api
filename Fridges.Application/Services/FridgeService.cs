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

    public FridgeService(IFridgeRepository repository, IFridgeModelRepository fridgeModelRepository, IProductRepository productRepository)
    {
        _repository = repository;
        _fridgeModelRepository = fridgeModelRepository;
        _productRepository = productRepository;
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
            OwnerName = Fridge.OwnerName,
            FridgeModel = _fridgeModelRepository.GetFridgeModelById(Fridge.FridgeModelId)
        };
        _repository.InsertFridge(fridge);

        return fridge;
    }

    public void AddProducts(AddProductsDto addProductsDto)
    {
        FridgeProduct fridgeProduct = new()
        {
            Fridge = _repository.GetFridgeById(addProductsDto.FridgeId),
            Product = _productRepository.GetProductById(addProductsDto.ProductId),
            Quantity = addProductsDto.Quantity
        };
        _repository.AddProducts(fridgeProduct);
    }
    
    public void RemoveProducts(Guid FridgeId, Guid ProductId)
    {
        RemoveProductsDto removeProductsDto = new()
        {
            FridgeId = FridgeId,
            ProductId = ProductId
        };

        _repository.RemoveProducts(removeProductsDto);
    }

    public void UpdateFridge(Fridge Fridge)
    {
        _repository.UpdateFridge(Fridge);
    }

    public void DeleteFridge(Guid FridgeId)
    {
        _repository.DeleteFridge(FridgeId);
    }

    public IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid FridgeId)
    {
        return _repository.GetProductsByFridgeId(FridgeId);
    }
}
