using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Interfaces.Services;

public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product GetProductById(Guid ProductId);
    Product CreateProduct(ProductCreateDto Product);
    void UpdateProduct(UpdateProductDto updateProductDto);
    void DeleteProduct(Guid ProductID);
}
