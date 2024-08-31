// See https://aka.ms/new-console-template for more information
using Rmauro.Optimizations.ObjectMappers.Generator;

Console.WriteLine("Hello, World!");


//StringExtensions.SayHello();

"Je;;p".SayHello();

var source = new SampleModel() { Id = 20, Name = "Hello World" };

var target = new SampleModel();

MapSampleModel.Map(ref source, ref target);

Console.WriteLine("Id {0} Name {1}", target.Id, target.Name);