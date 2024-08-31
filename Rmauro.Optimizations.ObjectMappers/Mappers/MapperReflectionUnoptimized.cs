using System;

namespace Rmauro.Optimizations.ObjectMappers.Mappers;

public class MapperReflectionUnoptimized : MapperBase
{
    public override void MapTypes(Type source, Type target)
    {
    }

    public override void Copy(ref object source, ref object target)
    {
        var sourceType = source.GetType();
        var targetType = target.GetType();
        var propMap = GetMatchingProperties(sourceType, targetType);

        for (var i = 0; i < propMap.Count; i++)
        {
            var prop = propMap[i];
            var sourceValue = prop.SourceProperty.GetValue(source, null);
            prop.TargetProperty.SetValue(target, sourceValue, null);
        }
    }
}
