namespace Fridges.Domain.Exceptions;

public static class Exceptions
{
    public static NotFoundException productNotFound = new("Product not found.");
    public static NotFoundException fridgeNotFound = new("Fridge not found.");
    public static NotFoundException fridgeModelNotFound = new("Fridge model not found.");

    public static AlreadyExistsException productAlreadyExists = new("Product with this name already exists.");
    public static AlreadyExistsException fridgeAlreadyExists = new("Fridge with this name already exists.");
    public static AlreadyExistsException fridgeModelAlreadyExists = new("Fridge model with this name already exists.");

    public static NotAllowedException negativeProductQuantity = new("Current fridge does not have this many products.");
}
