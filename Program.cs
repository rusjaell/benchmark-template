using BenchmarkDotNet.Running;

namespace Benchmark;

public sealed class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ExampleBenchmark>();
    }
}