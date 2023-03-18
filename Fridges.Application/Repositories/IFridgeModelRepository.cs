using Fridges.Domain.Entities;

namespace Fridges.Application.Repositories;

public interface IFridgeModelRepository
{
    FridgeModel GetFridgeModelById(Guid id);
}
