``` ini

BenchmarkDotNet=v0.11.0, OS=Windows 10.0.17134.165 (1803/April2018Update/Redstone4)
Intel Core i7-4770K CPU 3.50GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3415988 Hz, Resolution=292.7411 ns, Timer=TSC
.NET Core SDK=2.1.202
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|  Method |                     Text |       Mean |     Error |    StdDev |
|-------- |------------------------- |-----------:|----------:|----------:|
| **Sprache** | **(([([(...)])])) [100201]** |   **448.6 ms** |  **8.964 ms** | **16.163 ms** |
|   Naive | (([([(...)])])) [100201] |   441.9 ms |  5.629 ms |  4.700 ms |
| **Sprache** | **([x,x(...)x,x]) [100201]** | **1,240.9 ms** | **23.085 ms** | **21.594 ms** |
|   Naive | ([x,x(...)x,x]) [100201] | 1,249.1 ms | 17.436 ms | 15.457 ms |
