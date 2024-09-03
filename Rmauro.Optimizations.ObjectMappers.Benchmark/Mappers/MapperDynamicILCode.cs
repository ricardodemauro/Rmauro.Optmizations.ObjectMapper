using System.Reflection.Emit;

namespace Rmauro.Optimizations.ObjectMappers.Mappers;

public class MapperDynamicILCode : MapperBase
{
    DynamicMethod _ilCode;

    public override void MapTypes(Type source, Type target)
    {
        if(_ilCode is not null) return;

        var key = GetMapKey(source, target);

        var args = new[] { source, target };
        var mod = typeof(Program).Module;

        var dm = new DynamicMethod(key, null, args, mod);
        var il = dm.GetILGenerator();
        var maps = GetMatchingProperties(source, target);

        foreach (var map in maps)
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.EmitCall(OpCodes.Callvirt, map.SourceProperty.GetGetMethod(), null);
            il.EmitCall(OpCodes.Callvirt, map.TargetProperty.GetSetMethod(), null);
        }
        il.Emit(OpCodes.Ret);
        _ilCode = dm;
    }

    public void Copy(RandomModel source, RandomModel target)
    {
        var args = new[] { source, target };
        _ilCode.Invoke(null, args);
    }

    public override MapperBase WithMappedType(Type source, Type target)
    {
        this.MapTypes(source, target);

        return this;
    }
}
