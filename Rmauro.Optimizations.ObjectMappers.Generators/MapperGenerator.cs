using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Rmauro.Optimizations.ObjectMappers.Generators;


[Generator]
public partial class MapperGenerator : IIncrementalGenerator
{
    const string NAMESPACE = "Rmauro.Optimizations.ObjectMappers.Generators";

    const string ATTRIBUTE_FULL_NAME = "Rmauro.Optimizations.ObjectMappers.Generators.MapperGeneratorAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(x =>
        {
            x.AddSource(
                "MapperExtensionsAttribute.g.cs",
                SourceText.From(MapperGenerator.AttributeClass,
                Encoding.UTF8));
        });

        IncrementalValuesProvider<Class2Generate?> enumsToGenerate = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                ATTRIBUTE_FULL_NAME,
                predicate: static (s, _) => true,
                transform: static (ctx, _) => GetClass2Generate(ctx.SemanticModel, ctx.TargetNode))
            .Where(static m => m is not null);


        // Generate source code for each enum found
        context.RegisterSourceOutput(enumsToGenerate,
            static (spc, source) => Execute(source, spc));
    }

    static Class2Generate? GetClass2Generate(SemanticModel semanticModel, SyntaxNode enumDeclarationSyntax)
    {
        // Get the semantic representation of the enum syntax
        if (semanticModel.GetDeclaredSymbol(enumDeclarationSyntax) is not INamedTypeSymbol symbol)
        {
            // something went wrong
            return null;
        }

        // Get the full type name of the enum e.g. Colour, 
        // or OuterClass<T>.Colour if it was nested in a generic type (for example)
        string classFullName = symbol?.ToString() ?? throw new ApplicationException("Could not get the symbol name");
        string className = symbol.Name;
        string @namespace = classFullName.Substring(0, classFullName.Length - (className.Length + 1));

        // Get all the members in the enum
        ImmutableArray<ISymbol> enumMembers = symbol.GetMembers();
        var members = new List<string>(enumMembers.Length);

        // Get all the fields from the enum, and add their name to the list
        foreach (ISymbol member in enumMembers)
        {
            // if (member is IFieldSymbol field && field.ConstantValue is not null)
            // {
            //     members.Add(member.Name);
            // }
            if (member is IPropertySymbol field 
                && field.SetMethod is not null 
                && field.GetMethod is not null)
            {
                members.Add(member.Name);
            }
        }

        return new Class2Generate(className, @namespace, members);
    }

    static void Execute(Class2Generate? class2Generate, SourceProductionContext context)
    {
        if (class2Generate is { } value)
        {
            // generate the source code and add it to the output
            string result = GenerateExtensionClass(value);

            // Create a separate partial class file for each enum
            context.AddSource(
                $"{value.ClassName}Extensions.g.cs",
                SourceText.From(result, Encoding.UTF8));
        }
    }

    public static string GenerateExtensionClass(Class2Generate class2Generate)
    {
        var sb = new StringBuilder();

        sb.Append($@"
namespace {class2Generate.Namespace}
{{
    public static partial class Map{class2Generate.ClassName}
    {{
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void Map(ref {class2Generate.ClassName} source, ref {class2Generate.ClassName} target)
        {{");

        foreach (string member in class2Generate.Values)
        {
            sb.Append($@"
            target.{member} = source.{member};");
        }

        sb.Append($@"
        }}
    }}
}}");
        return sb.ToString();
    }

}
