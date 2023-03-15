using Fridges.Domain.Entities;

namespace Fridges.Application.Interfaces.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetProducts();
    Product GetProductByID(Guid ProductId);
    void InsertProduct(Product Product);
    void UpdateProduct(Product Product);
    void DeleteProduct(Guid ProductID);
    void Save();
}
