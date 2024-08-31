namespace Rmauro.Optimizations.ObjectMappers.Mappers;

public class MapperDynamicGeneratedCode : MapperBase, ITypedCopy
{
    public override void Copy(ref object source, ref object target)
    {
        var s = (RandomModel)source;
        var t = (RandomModel)target;

        MapRandomModel.Map(ref s, ref t);
    }

    public override void MapTypes(Type source, Type target)
    {
        
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public void Copy(ref RandomModel source, ref RandomModel target)
    {
        MapRandomModel.Map(ref source, ref target);
    }
}
