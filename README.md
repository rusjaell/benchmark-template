# benchmark-template
This repository contains a basic template for benchmarking using [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet).

## Prerequisites

- .NET 6.0 or higher
- BenchmarkDotNet Nuget

## Example Benchmark Code

```csharp
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace Benchmark;

[MemoryDiagnoser]
public class ExampleBenchmark
{
    private List<int> _list;
    private HashSet<int> _hashSet;
    private Dictionary<int, int> _dictionary;

    [Params(10, 100, 1000, 10000)]
    public int Iterations;

    [GlobalSetup]
    public void Setup()
    {
        _list = new List<int>(Iterations);
        _hashSet = new HashSet<int>(Iterations);
        _dictionary = new Dictionary<int, int>(Iterations);

        for (var i = 0; i < Iterations; i++)
        {
            _list.Add(i);
            _hashSet.Add(i);
            _dictionary.Add(i, i);
        }
    }

    [Benchmark]
    public void IterateList()
    {
        foreach (var item in _list)
        {
            _ = item;
        }
    }

    [Benchmark]
    public void IterateHashSet()
    {
        foreach (var item in _hashSet)
        {
            _ = item;
        }
    }

    [Benchmark]
    public void IterateDictionary()
    {
        foreach (var item in _dictionary)
        {
            _ = item;
        }
    }

    [GlobalCleanup]
    public void Cleanup()
    {
    }
}
```

## Output Example
```text
| Method            | Iterations | Mean         | Error      | StdDev     | Allocated |
|------------------ |----------- |-------------:|-----------:|-----------:|----------:|
| IterateList       | 10         |     4.972 ns |  0.0219 ns |  0.0194 ns |         - |
| IterateHashSet    | 10         |     8.276 ns |  0.0502 ns |  0.0470 ns |         - |
| IterateDictionary | 10         |     7.613 ns |  0.0776 ns |  0.0606 ns |         - |
| IterateList       | 100        |    42.999 ns |  0.1308 ns |  0.1159 ns |         - |
| IterateHashSet    | 100        |    81.426 ns |  0.3933 ns |  0.3679 ns |         - |
| IterateDictionary | 100        |    76.505 ns |  0.1195 ns |  0.0998 ns |         - |
| IterateList       | 1000       |   363.505 ns |  0.3225 ns |  0.2859 ns |         - |
| IterateHashSet    | 1000       |   737.644 ns |  3.6676 ns |  3.4306 ns |         - |
| IterateDictionary | 1000       |   622.941 ns |  1.3246 ns |  1.1742 ns |         - |
| IterateList       | 10000      | 3,582.336 ns |  7.1044 ns |  5.5467 ns |         - |
| IterateHashSet    | 10000      | 7,297.675 ns | 33.8668 ns | 31.6790 ns |         - |
| IterateDictionary | 10000      | 7,187.029 ns | 32.3456 ns | 30.2561 ns |         - |
```
