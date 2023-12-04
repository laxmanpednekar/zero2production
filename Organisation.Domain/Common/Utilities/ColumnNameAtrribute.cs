namespace Organisation.Domain.Common.Utilities;

[AttributeUsage(AttributeTargets.Property)]
public class ColumnNameAttribute : Attribute
{
    public string NameValue { get; }

    public ColumnNameAttribute(string nameValue)
    {
        NameValue = nameValue;
    }
}