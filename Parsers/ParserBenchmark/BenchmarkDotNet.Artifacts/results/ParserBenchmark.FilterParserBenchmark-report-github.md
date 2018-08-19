``` ini

BenchmarkDotNet=v0.11.0, OS=Windows 10.0.17134.167 (1803/April2018Update/Redstone4)
Intel Core i7-4702MQ CPU 2.20GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=2143484 Hz, Resolution=466.5302 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (CoreCLR 4.6.26020.03, CoreFX 4.6.26018.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (CoreCLR 4.6.26020.03, CoreFX 4.6.26018.01), 64bit RyuJIT


```
|  Method |                     Text |     Mean |     Error |    StdDev |   Median |
|-------- |------------------------- |---------:|----------:|----------:|---------:|
| **Sprache** | **(([([(...)])])) [100201]** | **655.3 ms** |  **9.674 ms** |  **9.049 ms** | **652.2 ms** |
|   Naive | (([([(...)])])) [100201] | 651.9 ms | 12.898 ms | 27.487 ms | 639.8 ms |
| **Sprache** | **([x,x(...)x,x]) [100201]** | **206.8 ms** |  **1.667 ms** |  **1.559 ms** | **206.4 ms** |
|   Naive | ([x,x(...)x,x]) [100201] | 209.1 ms |  3.591 ms |  2.999 ms | 208.2 ms |
