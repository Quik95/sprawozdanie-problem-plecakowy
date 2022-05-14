using NUnit.Framework;

namespace Problem_plecakowy;

public class BruteForceWithBacktracking
{
    public static IEnumerable<bool> FindSolution(int capacity, IList<Item> items)
    {
        var res = new List<List<bool>>();
        _worker(capacity, items, res);

        return BruteForce.FindBestSolution(res, items, capacity);
    }
    
    private static void _worker(int capacity, IList<Item> items, List<List<bool>> results, List<bool>? current = null)
    {
        while (true)
        {
            current ??= new List<bool>();

            var (size, _) = BruteForce.calculateTotalValueAndWeight(items, current);
            if (size > capacity) return;

            if (current.Count == items.Count)
            {
                results.Add(current);
                return;
            }

            var newCurrent = new List<bool>(current) {false};
            _worker(capacity, items, results, newCurrent);

            newCurrent = new List<bool>(current) {true};
            current = newCurrent;
        }
    }
}