using AutoMapper;
using BenchmarkDotNet.Attributes;
using Rmauro.Optimizations.ObjectMappers.Mappers;

namespace Rmauro.Optimizations.ObjectMappers.Benchmark;

[WarmupCount(1)]
[GcServer(true)]
[MemoryDiagnoser]
public class ObjectMapperBenchmarks
{
    static readonly RandomModel source = new()
    {
        Id = 1,
        CustomerName = "John Doe",
        DeliveryAddress = "Lonely Souls Blvd. 1382",
        EstimatedDeliveryDate = DateTime.Now,
        OrderReference = "ODF/SDP/1929242111-237821"
    };

    static readonly RandomModel target = new();

    static readonly MapperConfiguration config = new(cfg => cfg.CreateMap<RandomModel, RandomModel>());

    static IMapper _mapper;

    static ObjectMapperBenchmarks()
    {
        _mapper = config.CreateMapper();
    }


    [Benchmark()]
    public RandomModel TestAutoMapper()
    {
        return _mapper.Map<RandomModel, RandomModel>(source, target);
    }


    static readonly MapperDynamicCode csharpDynamic = new MapperDynamicCode()
        .WithMappedType(source.GetType(), target.GetType()) as MapperDynamicCode;
    [Benchmark()]
    public RandomModel TestCSharpDynamic()
    {
        csharpDynamic.Copy(source, target);
        return target;
    }

    [Benchmark(Baseline = true)]
    public RandomModel TestManualAssign()
    {
        MapperManualAssign.Copy(source, target);
        return target;
    }

    static readonly MapperDynamicILCode ilAssign = new MapperDynamicILCode()
        .WithMappedType(source.GetType(), target.GetType()) as MapperDynamicILCode;
    [Benchmark()]
    public RandomModel TestILAssign()
    {
        ilAssign.Copy(source, target);
        return target;
    }

    static readonly MapperDynamicILCode reflectionOptimal = new MapperDynamicILCode()
        .WithMappedType(source.GetType(), target.GetType()) as MapperDynamicILCode;
    [Benchmark()]
    public RandomModel TestReflectionOptimalAssign()
    {
        reflectionOptimal.Copy(source, target);
        return target;
    }

    static readonly MapperDynamicILCode reflectionNaive = new MapperDynamicILCode()
        .WithMappedType(source.GetType(), target.GetType()) as MapperDynamicILCode;
    [Benchmark()]
    public RandomModel TestReflectionNaiveAssign()
    {
        reflectionNaive.Copy(source, target);
        return target;
    }

    [Benchmark()]
    public RandomModel TestGeneratorAssign()
    {
        MapRandomModel.MapTo(source, target);
        return target;
    }
}
