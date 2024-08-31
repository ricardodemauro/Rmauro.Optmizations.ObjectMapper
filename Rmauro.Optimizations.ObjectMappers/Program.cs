
using AutoMapper;
using Rmauro.Optimizations.ObjectMappers;
using Rmauro.Optimizations.ObjectMappers.Mappers;
using System.Diagnostics;

Console.WriteLine("Hello, World! Testing Different Mappers");

const string DoubleFixedPoint = "0.###################################################################################################################################################################################################################################################################################################################################################";

int rounds = 1_000_000;

var source = new RandomModel
{
    Id = 1,
    CustomerName = "John Doe",
    DeliveryAddress = "Lonely Souls Blvd. 1382",
    EstimatedDeliveryDate = DateTime.Now,
    OrderReference = "ODF/SDP/1929242111-237821"
};
var target = new RandomModel();

TestTypedCopy(source, target);
TestMappers(source, target);
TestAutoMapper(source, target);

Console.WriteLine(Environment.NewLine);
Console.WriteLine("Press any key to exit ...");


void TestMappers(object source, object target)
{
    var mappers = new MapperBase[]
    {
        new MapperReflectionUnoptimized(),
        new MapperReflectionOptimized(),
        new MapperDynamicCode(),
        new MapperDynamicILCode(),
        new MapperManualAssign(),
        new MapperDynamicGeneratedCode()
    };

    var sourceType = source.GetType();
    var targetType = target.GetType();
    var stopper = new Stopwatch();

    foreach (var mapper in mappers)
    {
        mapper.MapTypes(sourceType, targetType);

        stopper.Restart();

        for (var i = 0; i < rounds; i++)
        {
            mapper.Copy(ref source, ref target);
        }

        stopper.Stop();

        var time = stopper.ElapsedMilliseconds / (double)rounds;
        Console.WriteLine(mapper.GetType().Name + ": " + time.ToString(DoubleFixedPoint));
    }
}

void TestTypedCopy(RandomModel source, RandomModel target)
{
    var mappers = new ITypedCopy[]
    {
        new MapperManualAssign(),
        new MapperDynamicGeneratedCode()
    };

    var sourceType = source.GetType();
    var targetType = target.GetType();
    var stopper = new Stopwatch();

    foreach (var mapper in mappers)
    {
        stopper.Restart();

        for (var i = 0; i < rounds; i++)
        {
            mapper.Copy(ref source, ref target);
        }

        stopper.Stop();

        var time = stopper.ElapsedMilliseconds / (double)rounds;
        Console.WriteLine("Typed: " + mapper.GetType().Name + ": " + time.ToString(DoubleFixedPoint));
    }
}

void TestAutoMapper(RandomModel source, RandomModel target)
{
    var config = new MapperConfiguration(cfg => cfg.CreateMap<RandomModel, RandomModel>());

    var mapper = config.CreateMapper();

    _ = mapper.Map<RandomModel, RandomModel>(source, target);

    var stopper = new Stopwatch();

    stopper.Start();

    for (var i = 0; i < rounds; i++)
    {
        _ = mapper.Map<RandomModel, RandomModel>(source, target);
    }

    stopper.Stop();

    var time = stopper.ElapsedMilliseconds / (double)rounds;
    Console.WriteLine("AutoMapper: " + time.ToString(DoubleFixedPoint));
}