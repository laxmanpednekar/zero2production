using Organisation.Domain.Permission.Models;
using Organisation.Domain.User.Models;

namespace Organisation.Application.Common.Interfaces.Persistance;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<User> GetUserByEmail(string email);
    public Task<IEnumerable<Permission>> GetUserPermissions(User user);
}