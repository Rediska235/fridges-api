using System.Security.Authentication;

namespace Fridges.Domain.Exceptions;

public static class Exceptions
{
    public static NotFoundException productNotFound = new("Product not found.");
    public static NotFoundException fridgeNotFound = new("Fridge not found.");
    public static NotFoundException fridgeModelNotFound = new("Fridge model not found.");
    public static NotFoundException userNotFound = new("User not found.");
    public static NotFoundException roleNotFound = new("Role not found.");

    public static AlreadyExistsException productAlreadyExists = new("Product with this name already exists.");
    public static AlreadyExistsException fridgeAlreadyExists = new("Fridge with this name already exists.");
    public static AlreadyExistsException fridgeModelAlreadyExists = new("Fridge model with this name already exists.");
    public static AlreadyExistsException usernameIsTaken = new("This username is taken.");

    public static NotAllowedException notHaveThisManyProducts = new("Current fridge does not have this many products.");
    public static NotAllowedException negativeProductQuantity = new("You can't add new products with negative quantity.");

    public static InvalidCredentialException invalidCredential = new("Invalid username or password.");
    public static InvalidCredentialException invalidRefreshToken = new("Invalid refresh token.");
    public static InvalidCredentialException expiredRefreshToken = new("Expired refresh token.");
}
