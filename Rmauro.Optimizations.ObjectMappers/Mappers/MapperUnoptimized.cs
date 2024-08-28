using System;

namespace Rmauro.Optimizations.ObjectMappers.Mappers;

public class MapperUnoptimized : MapperBase
{
    public override void MapTypes(Type source, Type target)
    {
    }

    public override void Copy(object source, object target)
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

    public override TOut CopyIt<TIn, TOut>(TIn source)
    {
        var target = new TOut();

        var sourceType = source.GetType();
        var targetType = target.GetType();
        var propMap = GetMatchingProperties(sourceType, targetType);

        for (var i = 0; i < propMap.Count; i++)
        {
            var prop = propMap[i];
            var sourceValue = prop.SourceProperty.GetValue(source, null);
            prop.TargetProperty.SetValue(target, sourceValue, null);
        }
        return target;
    }
}
