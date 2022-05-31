using System.Collections;
using System.Globalization;
using Algorytmy;
using CsvHelper;

namespace Benchmarks
{
    public class Program
    {
        private static readonly int[] iterations = { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        private static readonly double[] density = { 0.25, 0.5, 0.75 };

        public static void Main(string[] args)
        {
            var capacity_25 = new List<IterationResult>();
            var capacity_50 = new List<IterationResult>();
            var capacity_75 = new List<IterationResult>();

            foreach (var n in iterations)
            {
                foreach (var b in density)
                {
                    Console.WriteLine($"Measuring N: {n}, B: {b:F2}");

                    var data = RandomItemGenerator.Generate(n);
                    var capacity = RandomItemGenerator.GetKnapsackCapacity(data, b);

                    var bf1 = Measure(BruteForce.FindSolution, capacity, data, 2);
                    var bf2 = Measure(BruteForceWithBacktracking.FindSolution, capacity, data, 5);
                    var dyn = Measure(DynamicProgramming.FindSolution, capacity, data);
                    var rat = Measure(Heurystyki.Ratio, capacity, data, 100_000);

                    var res = new IterationResult(n, bf1, bf2, dyn, rat);
                    switch (b)
                    {
                        case 0.25:
                            capacity_25.Add(res);
                            break;
                        case 0.5:
                            capacity_50.Add(res);
                            break;
                        default:
                            capacity_75.Add(res);
                            break;
                    }
                }
            }

            try
            {
                File.Delete("wyniki_025.csv");
            }
            catch (DirectoryNotFoundException)
            {
            }

            using var writer = new StreamWriter("wyniki_025.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords((IEnumerable)capacity_25);

            try
            {
                File.Delete("wyniki_050.csv");
            }
            catch (DirectoryNotFoundException)
            {
            }

            using var writer1 = new StreamWriter("wyniki_050.csv");
            using var csv1 = new CsvWriter(writer1, CultureInfo.InvariantCulture);
            csv1.WriteRecords((IEnumerable)capacity_50);

            try
            {
                File.Delete("wyniki_075.csv");
            }
            catch (DirectoryNotFoundException)
            {
            }

            using var writer2 = new StreamWriter("wyniki_075.csv");
            using var csv2 = new CsvWriter(writer2, CultureInfo.InvariantCulture);
            csv2.WriteRecords((IEnumerable)capacity_75);
        }

        private record IterationResult(int n, double bruteForce, double bruteForceBacktracking, double dynamic, double ratio);

        private static double Measure(Func<int, List<Item>, IEnumerable<bool>> func, int capacity, List<Item> data, int n = 100)
        {
            var measurements = new List<double>();
            foreach (var i in Enumerable.Range(1, n + 3))
            {
                var start = System.Diagnostics.Stopwatch.StartNew();
                func(capacity, data);
                start.Stop();
                if (i is 1 or 2 or 3)
                    continue; // JIT warm-up
                measurements.Add(start.Elapsed.TotalSeconds);
            }

            return measurements.Average();
        }
    }
}
