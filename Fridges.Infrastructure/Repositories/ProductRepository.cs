using Fridges.Application.Repositories;
using Fridges.Domain.Entities;
using Fridges.Domain.Exceptions;
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

    public Product GetProductById(Guid productId)
    {
        var product = _db.Products.FirstOrDefault(p => p.Id == productId);
        if (product == null)
        {
            throw Exceptions.productNotFound;
        }

        return product;
    }

    public Product GetProductByName(string name)
    {
        var product = _db.Products.FirstOrDefault(p => p.Name == name);
        if (product == null)
        {
            throw Exceptions.productNotFound;
        }

        return product;
    }

    public void InsertProduct(Product product)
    {
        _db.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        _db.Update(product);
    }

    public void DeleteProduct(Guid productId)
    {
        var product = GetProductById(productId);
        _db.Remove(product);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
