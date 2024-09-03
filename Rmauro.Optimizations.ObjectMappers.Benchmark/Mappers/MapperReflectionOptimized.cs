namespace Rmauro.Optimizations.ObjectMappers.Mappers;

public class MapperReflectionOptimized : MapperBase
{
    PropertyMap[] _props;

    public override void MapTypes(Type source, Type target)
    {
        if(_props is not null) return;
        
        var props = GetMatchingProperties(source, target);
        _props = [.. props];
    }

    public void Copy(RandomModel source, RandomModel target)
    {
        var propMap = _props;

        for (var i = 0; i < propMap.Length; i++)
        {
            var prop = propMap[i];
            var sourceValue = prop.SourceProperty.GetValue(source, null);
            prop.TargetProperty.SetValue(target, sourceValue, null);
        }
    }

    public override MapperBase WithMappedType(Type source, Type target)
    {
        this.MapTypes(source, target);

        return this;
    }
}
