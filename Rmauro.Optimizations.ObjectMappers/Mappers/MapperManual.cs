using System;

namespace Rmauro.Optimizations.ObjectMappers.Mappers;

public class MapperManual : MapperBase
{
    public override void MapTypes(Type source, Type target)
    {}

    public override void Copy(object source, object target)
    {
        var s = (OrderModel)source;
        var t = (OrderModel)target;

        t.Id = s.Id;

        t.CustomerName = s.CustomerName;

        t.DeliveryAddress = s.DeliveryAddress;

        t.EstimatedDeliveryDate = s.EstimatedDeliveryDate;

        t.OrderReference = s.OrderReference;
    }

    public override TOut CopyIt<TIn, TOut>(TIn source)
    {
        var target = new TOut();

        var s = source as OrderModel;
        var t = target as OrderModel;

        t.Id = s.Id;
        t.CustomerName = s.CustomerName;
        t.DeliveryAddress = s.DeliveryAddress;
        t.EstimatedDeliveryDate = s.EstimatedDeliveryDate;
        t.OrderReference = s.OrderReference;

        return t as TOut;
    }
}
