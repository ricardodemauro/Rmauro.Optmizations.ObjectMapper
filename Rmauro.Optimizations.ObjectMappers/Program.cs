
using AutoMapper;
using Rmauro.Optimizations.ObjectMappers;
using Rmauro.Optimizations.ObjectMappers.Mappers;
using System.Diagnostics;

Console.WriteLine("Hello, World! Testing Different Mappers");

const string DoubleFixedPoint = "0.###################################################################################################################################################################################################################################################################################################################################################";


var source = new RandomModel
{
    Id = 1,
    CustomerName = "John Doe",
    DeliveryAddress = "Lonely Souls Blvd. 1382",
    EstimatedDeliveryDate = DateTime.Now,
    OrderReference = "ODF/SDP/1929242111-237821"
};
var target = new RandomModel();

TestMappers(source, target);
//TestMappersV2(source);
TestAutoMapper(source, target);

Console.WriteLine(Environment.NewLine);
Console.WriteLine("Press any key to exit ...");
Console.ReadKey();


static void TestMappers(object source, object target)
{
    var mappers = new MapperBase[]
    {
        new MapperReflectionUnoptimized(),
        new MapperReflectionOptimized(),
        new MapperDynamicCode(),
        new MapperDynamicILCode(),
        new MapperManualAssign()
    };

    var sourceType = source.GetType();
    var targetType = target.GetType();
    var stopper = new Stopwatch();
    var testRuns = 1000000;

    foreach (var mapper in mappers)
    {
        mapper.MapTypes(sourceType, targetType);

        stopper.Restart();

        for (var i = 0; i < testRuns; i++)
        {
            mapper.Copy(source, target);
        }

        stopper.Stop();

        var time = stopper.ElapsedMilliseconds / (double)testRuns;
        Console.WriteLine(mapper.GetType().Name + ": " + time.ToString(DoubleFixedPoint));
    }
}

static void TestAutoMapper(RandomModel source, RandomModel target)
{
    var config = new MapperConfiguration(cfg => cfg.CreateMap<RandomModel, RandomModel>());

    var mapper = config.CreateMapper();

    _ = mapper.Map<RandomModel, RandomModel>(source, target);

    var stopper = new Stopwatch();
    var testRuns = 1000000;

    stopper.Start();

    for (var i = 0; i < testRuns; i++)
    {
        _ = mapper.Map<RandomModel, RandomModel>(source, target);
    }

    stopper.Stop();

    var time = stopper.ElapsedMilliseconds / (double)testRuns;
    Console.WriteLine("AutoMapper: " + time);
}