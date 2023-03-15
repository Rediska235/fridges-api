using Fridges.Domain.Entities;

namespace Fridges.Application.Interfaces.Services;

public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product GetProductByID(Guid ProductId);
    void CreateProduct(Product Product);
    void UpdateProduct(Product Product);
    void DeleteProduct(Guid ProductID);
}
