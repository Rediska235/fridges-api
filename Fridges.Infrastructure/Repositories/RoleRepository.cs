using Fridges.Application.Repositories;
using Fridges.Domain.Entities;
using Fridges.Infrastructure.Data;

namespace Fridges.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _db;

    public RoleRepository(AppDbContext db)
    {
        _db = db;
    }

    public IEnumerable<Role> GetRoles()
    {
        return _db.Roles;
    }

    public Role GetRoleByName(string roleName)
    {
        return _db.Roles.FirstOrDefault(r => r.Title == roleName);
    }
}
