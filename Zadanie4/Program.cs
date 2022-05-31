using System;
using System.Collections;
using System.Globalization;
using Algorytmy;
using CsvHelper;

namespace Zadanie4
{
    internal static class Program
    {
        private static readonly int[] N = {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000};
        private static readonly double[] B = {0.25, 0.5, 0.75};

        private static void Main()
        {
            var res = new List<AverageError>();

            foreach (var n in N)
            {
                var randomKnapsack = RandomItemGenerator.Generate(n);
                foreach (var b in B)
                {
                    var capacity = RandomItemGenerator.GetKnapsackCapacity(randomKnapsack, b);

                    var startTime = System.Diagnostics.Stopwatch.StartNew();
                    var reference = DynamicProgramming.FindSolution(capacity, randomKnapsack);
                    var (_, referenceValue) = BruteForce.CalculateTotalSizeAndValue(randomKnapsack, reference);
                    startTime.Stop();
                    Console.WriteLine($"Getting reference for N: {n}, b: {b} took: {startTime.ElapsedMilliseconds}ms");


                    startTime = System.Diagnostics.Stopwatch.StartNew();
                    var (_, randomValue) = BruteForce.CalculateTotalSizeAndValue(randomKnapsack,
                        Heurystyki.Random(capacity, randomKnapsack));
                    startTime.Stop();
                    Console.WriteLine($"Random took for N: {n}, b: {b} took: {startTime.ElapsedMilliseconds}ms");

                    startTime = System.Diagnostics.Stopwatch.StartNew();
                    var (_, minimumValue) = BruteForce.CalculateTotalSizeAndValue(randomKnapsack,
                        Heurystyki.MinimumSize(capacity, randomKnapsack));
                    startTime.Stop();
                    Console.WriteLine($"Minimum took for N: {n}, b: {b} took: {startTime.ElapsedMilliseconds}ms");

                    startTime = System.Diagnostics.Stopwatch.StartNew();
                    var (_, maximumValue) = BruteForce.CalculateTotalSizeAndValue(randomKnapsack,
                        Heurystyki.MaximumValue(capacity, randomKnapsack));
                    startTime.Stop();
                    Console.WriteLine($"Maximum took for N: {n}, b: {b} took: {startTime.ElapsedMilliseconds}ms");

                    var (_, ratioValue) =
                        BruteForce.CalculateTotalSizeAndValue(randomKnapsack,
                            Heurystyki.Ratio(capacity, randomKnapsack));
                    startTime.Stop();
                    Console.WriteLine($"Ratio took for N: {n}, b: {b} took: {startTime.ElapsedMilliseconds}ms");

                    res.Add(
                        new AverageError(
                            n, b,
                            (referenceValue - randomValue) / (double) referenceValue,
                            (referenceValue - minimumValue) / (double) referenceValue,
                            (referenceValue - maximumValue) / (double) referenceValue,
                            (referenceValue - ratioValue) / (double) referenceValue)
                    );
                    Console.WriteLine($"N: {n}, b: {b} finished in: {startTime.ElapsedMilliseconds}ms");
                }
            }

            try
            {
                File.Delete("wyniki.csv");
            }
            catch (DirectoryNotFoundException)
            {
            }
            catch (IOException e)
            {
                Console.WriteLine("Cannot delete file wyniki.csv");
                Console.WriteLine($"Error message: {e.Message}");

                Environment.Exit(1);
            }

            using var writer = new StreamWriter("wyniki.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords((IEnumerable) res);
        }

        private record AverageError(int N, double B, double Random, double MinimumSize, double MaximumValue,
            double Ratio);
    }
}