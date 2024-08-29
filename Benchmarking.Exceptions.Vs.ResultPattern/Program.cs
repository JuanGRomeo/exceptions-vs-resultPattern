using BenchmarkDotNet.Running;
using Benchmarking.Exceptions.Vs.ResultPattern;

var summary = BenchmarkRunner.Run<ExceptionsVsResultBenchmark>(new Config());