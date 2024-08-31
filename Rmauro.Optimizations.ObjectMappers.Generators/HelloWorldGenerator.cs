using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Rmauro.Optimizations.ObjectMappers.Generators;

[Generator]
public class HelloWorldGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(x => {
            x.AddSource(
                "StringExtensions.g.cs", 
                SourceText.From(BuildClass(), 
                Encoding.UTF8));
        });
    }

    static string BuildClass()
    {
        const string s = @"
namespace System
{
    public static class StringExtensions
    {
        public static void SayHello(this string @string) => System.Console.WriteLine(@string);
    }
}
        ";

        return s;
    }
}
