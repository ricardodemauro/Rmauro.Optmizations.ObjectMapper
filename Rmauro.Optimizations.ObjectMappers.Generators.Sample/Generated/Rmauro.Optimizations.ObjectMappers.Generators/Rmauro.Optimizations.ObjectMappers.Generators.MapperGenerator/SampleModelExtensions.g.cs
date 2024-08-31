
namespace Rmauro.Optimizations.ObjectMappers.Generator
{
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static partial class MapSampleModel
    {

        public static void Map(ref SampleModel source, ref SampleModel target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
        }
    }
}