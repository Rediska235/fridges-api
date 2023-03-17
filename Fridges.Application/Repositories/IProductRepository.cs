using Fridges.Domain.Entities;

namespace Fridges.Application.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetProducts();
    Product GetProductById(Guid ProductId);
    void InsertProduct(Product Product);
    void UpdateProduct(Product Product);
    void DeleteProduct(Guid ProductId);
    void Save();
}
