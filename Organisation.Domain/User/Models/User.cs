using Organisation.Domain.Common.Models;
using Organisation.Domain.Common.Utilities;

namespace Organisation.Domain.User.Models;

[TableName("tblUserDetails")]
public class User : IDbEntity
{
    [PrimaryKey]
    [ColumnName("Id")]
    public string Id { get; set; } = ShortGuid.NewGuid();

    [ColumnName("UserName")]
    public string UserName { get; set; } = string.Empty;

    [DistinguishingUniqueKey]
    [ColumnName("Email")]
    public string Email { get; set; } = string.Empty;

    [ColumnName("PasswordHash")]
    public string PasswordHash { get; set; } = string.Empty;

    [ColumnName("RefreshToken")]
    public string? RefreshToken { get; set; } = string.Empty;

    [ColumnName("RefreshTokenExpiryDate")]
    public DateTime? RefreshTokenExpiryDate { get; set; }
}