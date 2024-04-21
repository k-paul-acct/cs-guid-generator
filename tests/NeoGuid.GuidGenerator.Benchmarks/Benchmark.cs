using BenchmarkDotNet.Attributes;

namespace NeoGuid.GuidGenerator.Benchmarks;

[MemoryDiagnoser(false)]
public class Benchmark
{
    private readonly Guid _guidV4 = GuidGenerator.NewV4();
    private readonly Guid _guidV7 = GuidGenerator.NewV7();

    [Benchmark]
    public GuidVersion Version()
    {
        return _guidV4.Version();
    }

    [Benchmark]
    public DateTime ExtractDateTime()
    {
        return _guidV7.ExtractDateTime();
    }

    [Benchmark]
    public Guid NewV7()
    {
        return GuidGenerator.NewV7();
    }

    [Benchmark]
    public Guid NewV4()
    {
        return GuidGenerator.NewV4();
    }
}