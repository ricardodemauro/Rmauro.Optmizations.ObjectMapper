using System.Runtime.CompilerServices;

namespace Rmauro.Optimizations.ObjectMappers.Mappers;

public abstract class MapperBase
{
    public abstract void MapTypes(Type source, Type target);

    public abstract void Copy(ref object source, ref object target);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected virtual IList<PropertyMap> GetMatchingProperties
        (Type sourceType, Type targetType)
    {
        var sourceProperties = sourceType.GetProperties();
        var targetProperties = targetType.GetProperties();

        var properties = (from s in sourceProperties
                          from t in targetProperties
                          where s.Name == t.Name &&
                                s.CanRead &&
                                t.CanWrite &&
                                s.PropertyType == t.PropertyType
                          select new PropertyMap
                          {
                              SourceProperty = s,
                              TargetProperty = t
                          }).ToList();
                          
        return properties;
    }

    protected virtual string GetMapKey(Type sourceType, Type targetType)
    {
        var keyName = "Copy_";
        keyName += sourceType.FullName.Replace(".", "_").Replace("+", "_");
        keyName += "_";
        keyName += targetType.FullName.Replace(".", "_").Replace("+", "_");

        return keyName;
    }
}
