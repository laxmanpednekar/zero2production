
namespace Organisation.Domain.Common.Utilities;

[AttributeUsage(AttributeTargets.Class)]
public class TableNameAttribute : Attribute
{
    public string NameValue { get; }

    public TableNameAttribute(string nameValue)
    {
        NameValue = nameValue;
    }
}
