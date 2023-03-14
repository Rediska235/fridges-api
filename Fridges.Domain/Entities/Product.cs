namespace Fridges.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? DefaultQuantity { get; set; }
    public List<FridgeProduct> FridgeProducts { get; set; }
}
