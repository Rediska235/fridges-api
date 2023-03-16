using Fridges.Application.Interfaces.Repositories;
using Fridges.Domain.Entities;
using Fridges.Infrastructure.Data;

namespace Fridges.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db)
    {
        _db = db;
    }

    public IEnumerable<Product> GetProducts()
    {
        return _db.Products;
    }

    public Product GetProductById(Guid ProductId)
    {
        return _db.Products.FirstOrDefault(p => p.Id == ProductId);
    }

    public void InsertProduct(Product Product)
    {
        _db.Add(Product);
        Save();
    }

    public void UpdateProduct(Product Product)
    {
        _db.Update(Product);
        Save();
    }

    public void DeleteProduct(Guid ProductId)
    {
        var product = GetProductById(ProductId);
        _db.Remove(product);
        Save();
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
