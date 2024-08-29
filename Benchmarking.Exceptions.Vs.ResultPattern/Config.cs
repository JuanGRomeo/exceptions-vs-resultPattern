using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;

namespace Benchmarking.Exceptions.Vs.ResultPattern;

public class Config : ManualConfig
{
    public Config()
    {
        AddExporter(MarkdownExporter.Default);
        AddExporter(HtmlExporter.Default);

        AddDiagnoser(new MemoryDiagnoser(new MemoryDiagnoserConfig(displayGenColumns: true)));
        AddDiagnoser(ThreadingDiagnoser.Default);
        AddDiagnoser(ExceptionDiagnoser.Default);

        AddLogger(ConsoleLogger.Default);

        AddColumnProvider(DefaultColumnProviders.Instance);
        AddColumn(StatisticColumn.AllStatistics);
    }
}
