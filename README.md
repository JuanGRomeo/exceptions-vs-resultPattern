# Exceptions Vs Result Pattern

Exercise to investigate how costly are exceptions comparing them against a Result Pattern.

It has been used K6 framework for Load testing and BenchmarkDotNet NuGet package for performance testing.

## Description

Having two endpoints that do the same thing, one using exceptions and the other using a Result Pattern, we will compare the performance of both.

## Load Test Scenarios

1. We simulate ramp-up of traffic from 1 to 20 users over 10 seconds.
2. Stay at 20 users for 50 seconds 


### How To Execute them

1. Assure you have k6 installed. If not, you can download it from [here](https://k6.io/docs/getting-started/installation/)
2. Set Exceptions.Vs.ResultPattern.Api as startup project
3. Run it without debugging (Ctrl + F5)
4. Open a terminal and navigate to the root of the project
5. Run the following command to execute the load test scenario you want to test. Assure you pass the correct file name.
	```bash
	k6 run api-test-add-book-v1.js
	```
6. Check the results in the terminal

## Results

### Get unexisting book using Exceptions

Around 4228 request per second

[![Get unexisting book using Exceptions](./apì-test-get-book-v1-results.png)](./apì-test-get-book-v1-results.png)

### Try add a book with invalid data using Exceptions

Around 3893 request per second

[![Try add a book with invalid data using Exceptions](./apì-test-add-book-v1-results.png)](./apì-test-add-book-v1-results.png)

---

### Get unexisting book using Result Pattern

Around 30296 request per second. **7 times faster** than Exceptions scenario

[![Get unexisting book using Result Pattern](./apì-test-get-book-v2-results.png)](./apì-test-get-book-v2-results.png)


### Try add a book with invalid data using Result Pattern

Around 23469 request per second. **6 times faster** than Exceptions scenario

[![Try add a book with invalid data using Exceptions](./apì-test-add-book-v2-results.png)](./apì-test-add-book-v2-results.png)

---

## Performance Test Scenario

### How To Execute them

1. Set Exceptions.Vs.ResultPattern.PerformanceTests as startup project
2. Build the solution in Release mode
3. Run it without debugging (Ctrl + F5)
4. Check the results in the terminal. Also the results are being exported in HTML and Markdown on ```\Benchmarking.Exceptions.Vs.ResultPattern\bin\Release\net8.0\BenchmarkDotNet.Artifacts\results``` folder



## Results

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4112/23H2/2023Update/SunValley3)
12th Gen Intel Core i5-1240P, 1 CPU, 16 logical and 12 physical cores
.NET SDK 8.0.304
  [Host]     : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2


 Method                  | Mean       | Error      | StdDev     | StdErr    | Min        | Q1         | Median     | Q3         | Max        | Op/s      | Gen0   | Exceptions | Completed Work Items | Lock Contentions | Allocated |
------------------------ |-----------:|-----------:|-----------:|----------:|-----------:|-----------:|-----------:|-----------:|-----------:|----------:|-------:|-----------:|---------------------:|-----------------:|----------:|
 AddBookOrThrowBenchmark | 501.399 μs | 11.9045 μs | 34.7260 μs | 3.5079 μs | 432.779 μs | 477.070 μs | 493.169 μs | 523.111 μs | 586.209 μs |   1,994.4 | 6.8359 |   100.0000 |                    - |                - |  42.97 KB |
 AddBookOrFailBenchmark  |  13.057 μs |  0.2603 μs |  0.5137 μs | 0.0742 μs |  11.850 μs |  12.688 μs |  13.016 μs |  13.378 μs |  14.353 μs |  76,588.3 | 7.9041 |          - |                    - |                - |  48.44 KB |
 GetBookOrThrowBenchmark | 425.688 μs |  8.3973 μs | 10.3127 μs | 2.1987 μs | 409.043 μs | 416.912 μs | 426.451 μs | 432.276 μs | 441.762 μs |   2,349.1 | 6.8359 |   100.0000 |                    - |                - |  43.75 KB |
 GetBookOrFailBenchmark  |   4.889 μs |  0.0918 μs |  0.1832 μs | 0.0262 μs |   4.497 μs |   4.765 μs |   4.871 μs |   4.971 μs |   5.317 μs | 204,528.6 | 3.1815 |          - |                    - |                - |  19.53 KB |


 AddBookOrFailBenchmark using Result Pattern is **38 times faster** than AddBookOrThrowBenchmark using Exceptions.

 GetBookOrFailBenchmark using Result Pattern is **87 times faster** than GetBookOrThrowBenchmark using Exceptions.
