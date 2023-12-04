
using Organisation.Domain.Common.Models;
using Organisation.Domain.Common.Utilities;

namespace Organisation.Domain.Models;

[TableName("tblEmployees")]
public sealed class Employee : IDbEntity
{
    [PrimaryKey]
    [ColumnName("Id")]
    public string Id { get; set; } = ShortGuid.NewGuid();

    [DistinguishingUniqueKey]
    [ColumnName("Name")]
    public string Name { get; set; } = string.Empty;

    [ColumnName("Age")]
    public int Age { get; set; }

    [ColumnName("Position")]
    public string Position { get; set; } = string.Empty;

    [ForeignKey]
    [ColumnName("CompanyId")]
    public string CompanyId { get; set; } = string.Empty;

    [ColumnName("IsDeleted")]
    public bool IsDeleted { get; set; }

    [ColumnName("CreatedOn")]
    public DateTime CreatedOn { get; set; }

    [ColumnName("ModifiedOn")]
    public DateTime ModifiedOn { get; set; }

    [ColumnName("Salary")]
    public decimal Salary { get; set; }
}