using Fridges.Domain.Entities;

namespace Fridges.Application.Repositories;

public interface IRoleRepository
{
    Role GetRoleByName(string roleName);
    IEnumerable<Role> GetRoles();
}
