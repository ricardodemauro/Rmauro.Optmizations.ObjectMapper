
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using Rmauro.Optimizations.ObjectMappers.Benchmark;

var summary = BenchmarkDotNet.Running.BenchmarkRunner.Run<ObjectMapperBenchmarks>(
    DefaultConfig.Instance
        .AddJob(Job.Default
            .WithRuntime(CoreRuntime.Core80)
            .WithJit(Jit.RyuJit)
            .WithGcMode(new GcMode()
            {
                Server = true,
                Concurrent = true
            })
            .WithLargeAddressAware(false)
        )
    );

//var summary = BenchmarkDotNet.Running.BenchmarkRunner.Run<ObjectMapperBenchmarks>();