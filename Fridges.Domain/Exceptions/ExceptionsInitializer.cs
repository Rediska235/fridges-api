namespace Fridges.Domain.Exceptions;

public class ExceptionsInitializer
{
    public NotFoundException productNotFound = new("Product not found.");
    public NotFoundException fridgeNotFound = new("Fridge not found.");

    public AlreadyExistsException productAlreadyExists = new("Product with this name already exists.");
    public AlreadyExistsException fridgeAlreadyExists = new("Fridge with this name already exists.");
}
