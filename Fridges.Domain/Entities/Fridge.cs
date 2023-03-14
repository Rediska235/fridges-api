namespace Fridges.Domain.Entities;

public class Fridge 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? OwnerName { get; set; }
    public FridgeModel FridgeModel { get; set; }
    public List<FridgeProduct> FridgeProducts { get; set; }
}
