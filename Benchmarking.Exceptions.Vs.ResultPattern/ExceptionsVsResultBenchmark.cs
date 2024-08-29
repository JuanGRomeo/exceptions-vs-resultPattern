using BenchmarkDotNet.Attributes;
using Exceptions.Vs.ResultPattern.Books;

namespace Benchmarking.Exceptions.Vs.ResultPattern
{
    public class ExceptionsVsResultBenchmark
    {
        private readonly BooksService _booksService;
        private List<int> data = new();

        public ExceptionsVsResultBenchmark()
        {
            _booksService = new BooksService();
        }

        [GlobalSetup]
        public void Setup()
        {
            data = Enumerable.Range(1, 100).ToList();
        }


        [Benchmark]
        public void AddBookOrThrowBenchmark()
        {
            foreach (var item in data)
            {
                try
                {
                    var request = new AddBookRequest { Title = "", Author = "" };
                    var book = _booksService.AddBookOrThrow(request);
                }
                catch (Exception)
                {
                }
            }
        }

        [Benchmark]
        public void AddBookOrFailBenchmark()
        {
            foreach (var item in data)
            {
                var request = new AddBookRequest { Title = "", Author = "" };
                var result = _booksService.AddBookOrFail(request);
            }
        }

        [Benchmark]
        public void GetBookOrThrowBenchmark()
        {
            foreach (var item in data)
            {
                try
                {
                    var book = _booksService.GetBookOrThrow(8);
                }
                catch (Exception)
                {
                }
            }
        }

        [Benchmark]
        public void GetBookOrFailBenchmark()
        {
            foreach (var item in data)
            {
                var result = _booksService.GetBookOrFail(8);
            }
        }

    }
}
