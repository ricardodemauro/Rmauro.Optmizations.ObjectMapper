namespace Rmauro.Optimizations.ObjectMappers.Generators;

public partial class MapperGenerator
{
    public const string AttributeClass = @"
namespace Rmauro.Optimizations.ObjectMappers.Generators
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class MapperGeneratorAttribute : Attribute
    {

    }
}";

    public record struct Class2Generate(string ClassName, string Namespace, List<string> Values);
}
