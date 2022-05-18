using System;
using System.Collections;
using System.Globalization;
using Algorytmy;
using CsvHelper;

namespace Zadanie4
{
    internal static class Program
    {
        private static readonly int[] N = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
        private static readonly double[] B = { 0.25, 0.5, 0.75 };

        private static void Main()
        {
            var res = new List<AverageError>();

            foreach (var n in N)
            {
                var randomKnapsack = RandomItemGenerator.Generate(n);
                foreach (var b in B)
                {
                    var currentSegmentStartTime = System.Diagnostics.Stopwatch.StartNew();
                    var capacity = RandomItemGenerator.GetKnapsackCapacity(randomKnapsack, b);

                    var reference = DynamicProgramming.FindSolution(capacity, randomKnapsack);
                    var (_, referenceValue) = BruteForce.CalculateTotalSizeAndValue(randomKnapsack, reference);

                    var (_, randomValue) = BruteForce.CalculateTotalSizeAndValue(randomKnapsack, Heurystyki.Random(capacity, randomKnapsack));
                    var (_, minimumValue) = BruteForce.CalculateTotalSizeAndValue(randomKnapsack, Heurystyki.MinimumSize(capacity, randomKnapsack));
                    var (_, maximumValue) = BruteForce.CalculateTotalSizeAndValue(randomKnapsack, Heurystyki.MaximumValue(capacity, randomKnapsack));
                    var (_, ratioValue) = BruteForce.CalculateTotalSizeAndValue(randomKnapsack, Heurystyki.Ratio(capacity, randomKnapsack));

                    res.Add(
                        new AverageError(
                            n, b,
                            (referenceValue - randomValue) / (double)referenceValue,
                            (referenceValue - minimumValue) / (double)referenceValue,
                            (referenceValue - maximumValue) / (double)referenceValue,
                            (referenceValue - ratioValue) / (double)referenceValue)
                        );
                    currentSegmentStartTime.Stop();
                    Console.WriteLine($"N: {n}, b: {b} finished in: {currentSegmentStartTime.ElapsedMilliseconds}ms");
                }
            }

            try
            {
                File.Delete("wyniki.csv");
            }
            catch (DirectoryNotFoundException) { }

            using var writer = new StreamWriter("wyniki.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords((IEnumerable)res);
        }

        private record AverageError(int n, double b, double random, double minimumSize, double maximumValue, double ratio);
    }
}
