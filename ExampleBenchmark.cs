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