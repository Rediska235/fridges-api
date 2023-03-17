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

    public Product GetProductById(Guid ProductId)
    {
        var product = _db.Products.FirstOrDefault(p => p.Id == ProductId);
        if(product == null)
        {
            throw Exceptions.productNotFound;
        }

        return product;
    }

    public void InsertProduct(Product Product)
    {
        _db.Add(Product);
    }

    public void UpdateProduct(Product Product)
    {
        _db.Update(Product);
    }

    public void DeleteProduct(Guid ProductId)
    {
        var product = GetProductById(ProductId);
        _db.Remove(product);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
