namespace Organisation.Domain.Common.Utilities;

[AttributeUsage(AttributeTargets.Property)]
public class NavigationAttribute : Attribute
{
    public Type? AssociatedType { get; }
    public string AssociatedProperty { get; }

    public NavigationAttribute(Type associatedType, string associatedProperty)
    {
        AssociatedType = associatedType;
        AssociatedProperty = associatedProperty;
    }
}
