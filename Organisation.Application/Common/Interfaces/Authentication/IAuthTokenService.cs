

using Organisation.Application.Common.DTO;
using Organisation.Domain.User.Models;

namespace Organisation.Application.Common.Interfaces.Authentication;

public interface IAuthTokenService
{
    Task<string> DoTokenCreationAsync(User user);
}
