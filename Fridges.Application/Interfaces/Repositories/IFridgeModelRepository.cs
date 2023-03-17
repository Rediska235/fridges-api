using Fridges.Domain.Entities;

namespace Fridges.Application.Interfaces.Repositories;

public interface IFridgeModelRepository
{
    FridgeModel GetFridgeModelById(Guid Id);
}
