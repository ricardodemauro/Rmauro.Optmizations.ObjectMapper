using System;

namespace Rmauro.Optimizations.ObjectMappers.Mappers;

public class MapperReflectionOptimized : MapperBase
{
    private readonly Dictionary<string, PropertyMap[]> _maps = new Dictionary<string, PropertyMap[]>();

    public override void MapTypes(Type source, Type target)
    {
        var key = GetMapKey(source, target);
        if (_maps.ContainsKey(key))
        {
            return;
        }

        var props = GetMatchingProperties(source, target);
        _maps.Add(key, props.ToArray());
    }

    public override void Copy(ref object source, ref object target)
    {
        var sourceType = source.GetType();
        var targetType = target.GetType();

        var key = GetMapKey(sourceType, targetType);
        if (!_maps.ContainsKey(key))
        {
            MapTypes(sourceType, targetType);
        }

        var propMap = _maps[key];

        for (var i = 0; i < propMap.Length; i++)
        {
            var prop = propMap[i];
            var sourceValue = prop.SourceProperty.GetValue(source, null);
            prop.TargetProperty.SetValue(target, sourceValue, null);
        }
    }
}
