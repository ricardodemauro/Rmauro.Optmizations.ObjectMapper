﻿
// <auto-generated />
namespace Rmauro.Optimizations.ObjectMappers
{
    public static partial class MapRandomModel
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void Map(ref RandomModel source, ref RandomModel target)
        {
            target.Id = source.Id;
            target.CustomerName = source.CustomerName;
            target.DeliveryAddress = source.DeliveryAddress;
            target.EstimatedDeliveryDate = source.EstimatedDeliveryDate;
            target.OrderReference = source.OrderReference;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void MapTo(RandomModel source, RandomModel target)
        {
            target.Id = source.Id;
            target.CustomerName = source.CustomerName;
            target.DeliveryAddress = source.DeliveryAddress;
            target.EstimatedDeliveryDate = source.EstimatedDeliveryDate;
            target.OrderReference = source.OrderReference;
        }
    }
}