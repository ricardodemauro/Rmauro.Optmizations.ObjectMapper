
using Rmauro.Optimizations.ObjectMappers.Generator;
namespace System
{
    public static partial class MapSampleModel
    {

        public static void Map(ref SampleModel source, ref SampleModel target)
        {        target.Id = source.Id;
        target.Name = source.Name;

        }
    }
}