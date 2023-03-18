using Fridges.Domain.Entities;

namespace Fridges.Application.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetProducts();
    Product GetProductById(Guid productId);
    Product GetProductByName(string productName);
    void InsertProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Guid productId);
    void Save();
}
