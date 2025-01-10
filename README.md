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
| IterateList       | 10         |     5.127 ns |  0.1289 ns |  0.1266 ns |         - |
| IterateHashSet    | 10         |     8.254 ns |  0.0490 ns |  0.0458 ns |         - |
| IterateDictionary | 10         |     7.585 ns |  0.0138 ns |  0.0115 ns |         - |
| IterateList       | 100        |    42.976 ns |  0.1372 ns |  0.1283 ns |         - |
| IterateHashSet    | 100        |    81.874 ns |  0.2449 ns |  0.2171 ns |         - |
| IterateDictionary | 100        |    68.762 ns |  0.2512 ns |  0.2227 ns |         - |
| IterateList       | 1000       |   366.145 ns |  1.2316 ns |  1.0285 ns |         - |
| IterateHashSet    | 1000       |   739.421 ns |  1.3844 ns |  1.2949 ns |         - |
| IterateDictionary | 1000       |   626.357 ns |  1.7643 ns |  1.4732 ns |         - |
| IterateList       | 10000      | 3,593.486 ns |  6.6263 ns |  5.5332 ns |         - |
| IterateHashSet    | 10000      | 7,324.924 ns | 14.0024 ns | 12.4127 ns |         - |
| IterateDictionary | 10000      | 7,203.216 ns | 11.1919 ns |  9.9214 ns |         - |
```