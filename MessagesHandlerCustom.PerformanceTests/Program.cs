using BenchmarkDotNet.Running;
using MessagesHandlerCustom.PerformanceTests;

var summary = BenchmarkRunner.Run<HandlerBenchmark>();
