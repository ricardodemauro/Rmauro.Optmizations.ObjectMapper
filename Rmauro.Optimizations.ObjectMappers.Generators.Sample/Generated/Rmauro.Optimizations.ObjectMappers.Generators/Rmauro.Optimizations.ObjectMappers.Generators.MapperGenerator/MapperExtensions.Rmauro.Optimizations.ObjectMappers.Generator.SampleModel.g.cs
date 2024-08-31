
namespace System
{
    public static partial class MapSampleModel
    {

        public static void Map(ref Rmauro.Optimizations.ObjectMappers.Generator.SampleModel source, ref Rmauro.Optimizations.ObjectMappers.Generator.SampleModel target)
        {        target.Id = source.Id;
        target.Name = source.Name;

        }
    }
}