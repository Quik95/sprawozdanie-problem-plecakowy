namespace Algorytmy;

public static class BruteForceWithBacktracking
{
    public static IEnumerable<bool> FindSolution(int capacity, IList<Item> items)
    {
        var res = new List<bool>(new bool[items.Count]);
        var best = 0;
        _worker(capacity, items, res, ref best, new List<bool>());

        return res;
    }

    private static void _worker(int capacity, IList<Item> items, IList<bool> result, ref int bestValue, List<bool> current, int currentSize = 0)
    {
            if (currentSize > capacity)
            {
                current.RemoveAt(current.Count - 1);
                return;
            }

            if (current.Count == items.Count)
            {
                var (_, value) = BruteForce.CalculateTotalSizeAndValue(items, current);
                if (value > bestValue)
                {
                    for (var i = 0; i < result.Count; i++) result[i] = current[i];
                    bestValue = value;
                }
                current.RemoveAt(current.Count-1);
                return;
            }

            current.Add(false);
            _worker(capacity, items, result, ref bestValue, current, currentSize);

            current.Add(true);
            _worker(capacity, items, result, ref bestValue, current, currentSize + items[current.Count - 1].Size);
            
            if (current.Count > 0)
                current.RemoveAt(current.Count-1);
    }
}