namespace Rmauro.Optimizations.ObjectMappers.Mappers;

public class MapperManualAssign : MapperBase
{
    public override void MapTypes(Type source, Type target)
    {}

    public override MapperBase WithMappedType(Type source, Type target)
    {
        this.MapTypes(source, target);

        return this;
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void Copy(RandomModel source, RandomModel target)
    {
        target.Id = source.Id;

        target.CustomerName = source.CustomerName;

        target.DeliveryAddress = source.DeliveryAddress;

        target.EstimatedDeliveryDate = source.EstimatedDeliveryDate;

        target.OrderReference = source.OrderReference;
    }
}
