using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace Rmauro.Optimizations.ObjectMappers.Mappers;

public class MapperDynamicCode : MapperBase
{
    Type generatedCode;

    public override void MapTypes(Type source, Type target)
    {
        if(generatedCode is not null) return;

        var key = GetMapKey(source, target);

        var builder = new StringBuilder();
        builder.Append($@"
        
        namespace Copy 
        {{
            public class {key}
            {{
                public static void CopyProps(
                    {target.FullName.Replace("+", ".")} source, 
                    {target.FullName.Replace("+", ".")} target)
                {{
        ");

        var map = GetMatchingProperties(source, target);
        foreach (var item in map)
        {
            builder.AppendLine($@"target.{item.TargetProperty.Name} = source.{item.SourceProperty.Name};");
        }

        builder.Append($@"
                }}
            }}
        }}");

        var syntaxTree = CSharpSyntaxTree.ParseText(builder.ToString());

        string assemblyName = Path.GetRandomFileName();
        var refPaths = new[]
        {
            typeof(System.Object).GetTypeInfo().Assembly.Location,
            typeof(Console).GetTypeInfo().Assembly.Location,
            Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Runtime.dll"),
            this.GetType().GetTypeInfo().Assembly.Location
        };

        MetadataReference[] references = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();

        var compilation = CSharpCompilation.Create(
            assemblyName,
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary, 
                optimizationLevel: OptimizationLevel.Release, 
                //checkOverflow: false, 
                platform: Platform.X64));

        using var ms = new MemoryStream();

        EmitResult result = compilation.Emit(ms);
        ms.Seek(0, SeekOrigin.Begin);

        Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
        generatedCode = assembly.GetType("Copy." + key);
    }

    public void Copy(RandomModel source, RandomModel target)
    {
        var flags = BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod;
        var args = new[] { source, target };
        generatedCode.InvokeMember("CopyProps", flags, null, null, args);
    }

    public override MapperBase WithMappedType(Type source, Type target)
    {
        this.MapTypes(source, target);

        return this;
    }
}
