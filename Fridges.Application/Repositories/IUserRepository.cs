using Fridges.Domain.Entities;

namespace Fridges.Application.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetUsers();
    User GetUserByUsername(string username);
    User GetUserByRefreshToken(string refreshToken);
    void InsertUser(User user);
    void UpdateUser(User user);
    void Save();
}
