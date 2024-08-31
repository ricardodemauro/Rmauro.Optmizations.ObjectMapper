namespace Rmauro.Optimizations.ObjectMappers.Mappers;

public class MapperManualAssign : MapperBase, ITypedCopy
{
    public override void MapTypes(Type source, Type target)
    {}

    public override void Copy(ref object source, ref object target)
    {
        var s = (RandomModel)source;
        var t = (RandomModel)target;

        t.Id = s.Id;

        t.CustomerName = s.CustomerName;

        t.DeliveryAddress = s.DeliveryAddress;

        t.EstimatedDeliveryDate = s.EstimatedDeliveryDate;

        t.OrderReference = s.OrderReference;
    }

    public void Copy(ref RandomModel source, ref RandomModel target)
    {
        target.Id = source.Id;

        target.CustomerName = source.CustomerName;

        target.DeliveryAddress = source.DeliveryAddress;

        target.EstimatedDeliveryDate = source.EstimatedDeliveryDate;

        target.OrderReference = source.OrderReference;
    }
}
