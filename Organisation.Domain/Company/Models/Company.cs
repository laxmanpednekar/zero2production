
using Organisation.Domain.Common.Models;
using Organisation.Domain.Common.Utilities;
namespace Organisation.Domain.Models;

[TableName("tblCompanies")]
public sealed class Company : IDbEntity
{
    [PrimaryKey]
    [ColumnName("Id")]
    public string Id { get; set; } = ShortGuid.NewGuid();

    [DistinguishingUniqueKey]
    [ColumnName("Name")]
    public string Name { get; set; } = string.Empty;

    [ColumnName("Address")]
    public string Address { get; set; } = string.Empty;

    [ColumnName("Country")]
    public string Country { get; set; } = string.Empty;

    [ColumnName("IsDeleted")]
    public bool IsDeleted { get; set; }

    [Navigation(typeof(Employee), "CompanyId")]
    public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
}
