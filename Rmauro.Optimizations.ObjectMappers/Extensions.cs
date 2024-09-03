using System.Reflection;
using System.Runtime.CompilerServices;
using Rmauro.Optimizations.ObjectMappers.Generators;

namespace Rmauro.Optimizations.ObjectMappers;

public record struct PropertyMap(PropertyInfo SourceProperty, PropertyInfo TargetProperty);

[MapperGenerator]
public class RandomModel
{
    public int Id { get; set; }

    public string CustomerName { get; set; }

    public string DeliveryAddress { get; set; }

    public DateTime EstimatedDeliveryDate { get; set; }

    public string OrderReference { get; set; }
}

public static class ObjectExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PropertyMap[] GetMatchingProps(this object source, object target)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(target);

        var sourceProperties = source.GetType().GetProperties();
        var targetProperties = target.GetType().GetProperties();

        var properties = 
        (
            from s in sourceProperties
            from t in targetProperties
            where s.Name == t.Name &&
                s.CanRead &&
                t.CanWrite &&
                s.PropertyType.IsPublic &&
                t.PropertyType.IsPublic &&
                s.PropertyType == t.PropertyType &&
                (
                    (s.PropertyType.IsValueType && t.PropertyType.IsValueType) 
                    || 
                    (s.PropertyType == typeof(string) && t.PropertyType == typeof(string))
                )
                select new PropertyMap
                {
                    SourceProperty = s,
                    TargetProperty = t
                }
        ).ToArray();

        return properties;
    }
}
