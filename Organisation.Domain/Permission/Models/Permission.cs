

using Organisation.Domain.Common.Models;
using Organisation.Domain.Common.Utilities;

namespace Organisation.Domain.Permission.Models;

[TableName("tblPermissions")]
public sealed class Permission : IDbEntity
{
    [PrimaryKey]
    [ColumnName("Id")]
    public string Id { get; set; } = ShortGuid.NewGuid();

    [ColumnName("Name")]
    public string Name { get; set; } = string.Empty;

    [ColumnName("IsDeleted")]
    public bool IsDeleted { get; set; }
}

