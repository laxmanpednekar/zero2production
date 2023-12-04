

using Organisation.Domain.Common.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Organisation.Application.Common.Utilities;

public static class Extensions
{
    public static string GetDbTableName(this Type type)
    {
        return type.GetCustomAttribute<TableNameAttribute>()?.NameValue ?? string.Empty;
    }

    public static string GetDbTableColumnNames(this Type type, string[] selectedProperties)
    {
        if (selectedProperties.Length < 1)
            return string.Join(",", type.GetProperties().Select(p => p.GetDbColumnName())).TrimEnd(',');
        else
            return string.Join(",", type.GetProperties().Where(p => selectedProperties.ToLowerInvariant().Contains(p.Name.ToLowerInvariant())).Select(p => p.GetDbColumnName())).TrimEnd(',');
    }

    public static string GetColumnValuesForInsert<T>(this Type type, T obj)
    {
        return string.Join(",", type.GetColumnProperties().Select(p => $"'{p.GetValue(obj)}'"));
    }

    public static string GetColumnValuesForUpdate<T>(this Type type, T obj)
    {
        return string.Join(",", type.GetNonPrimaryKeyColumnProperties().Select(p => $"{p.GetDbColumnName()}='{p.GetValue(obj)}'"));
    }

    public static string GetDbColumnName(this PropertyInfo propertyInfo)
    {
        return propertyInfo.GetCustomAttribute<ColumnNameAttribute>()?.NameValue ?? string.Empty;
    }

    public static IEnumerable<string> ToLowerInvariant(this string[] source)
    {
        foreach (var item in source)
            yield return item.ToLowerInvariant();
    }

    public static IEnumerable<PropertyInfo> GetColumnProperties(this Type type)
    {
        return type.GetProperties().Where(p => p.GetCustomAttribute<NavigationAttribute>() is null);
    }

    public static IEnumerable<PropertyInfo> GetNonPrimaryKeyColumnProperties(this Type type)
    {
        return type.GetProperties().Where(p => p.GetCustomAttribute<PrimaryKeyAttribute>() is null && p.GetCustomAttribute<NavigationAttribute>() is null);
    }

    public static IEnumerable<AssociatedType> GetAssociatedTypes(this Type type)
    {
        foreach (var associationAttribute in type.GetProperties().Where(p => p.GetCustomAttribute<NavigationAttribute>() is not null).Select(p => p.GetCustomAttribute<NavigationAttribute>()))
            yield return new AssociatedType(associationAttribute.AssociatedType, associationAttribute.AssociatedType.GetProperty(associationAttribute.AssociatedProperty));

    }

    public static string GetDistinguishingUniqueKeyName(this Type type)
    {
        return type.GetProperties().Where(p => p.GetCustomAttribute<DistinguishingUniqueKeyAttribute>() is not null).FirstOrDefault().Name;
    }

    public static IQueryable<IDbEntity> OrderByCustom<IDbEntity>(this IQueryable<IDbEntity> items, string sortBy, string sortOrder)
    {
        var type = typeof(IDbEntity);
        var expression2 = Expression.Parameter(type, "t");
        var property = type.GetProperty(sortBy);
        var expression1 = Expression.MakeMemberAccess(expression2, property);
        var lambda = Expression.Lambda(expression1, expression2);
        var result = Expression.Call(
            typeof(Queryable),
            sortOrder == "desc" ? "OrderByDescending" : "OrderBy",
            new Type[] { type, property.PropertyType },
            items.Expression,
            Expression.Quote(lambda));

        return items.Provider.CreateQuery<IDbEntity>(result);
    }

    public static bool IsValidEmail(this string email)
    {
        var regex = new Regex(@"^\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b$");
        return regex.IsMatch(email);
    }

    public static bool IsValidPassword(this string password)
    {
        var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()-+])(?=\S+$).{8,}$");
        return regex.IsMatch(password);
    }
}