using BenchmarkDotNet.Running;
using MessagesTrader.PerformanceTests;

var summary = BenchmarkRunner.Run<HandlerBenchmark>();
