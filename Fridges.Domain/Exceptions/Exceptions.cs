namespace Fridges.Domain.Exceptions;

public static class Exceptions
{
    public static NotFoundException productNotFound = new("Product not found.");
    public static NotFoundException fridgeNotFound = new("Fridge not found.");

    public static AlreadyExistsException productAlreadyExists = new("Product with this name already exists.");
    public static AlreadyExistsException fridgeAlreadyExists = new("Fridge with this name already exists.");
}
