namespace Algorytmy;

public static class BruteForce
{
    public static IEnumerable<bool> FindSolution(int capacity, IList<Item> items)
    {
        var bestCombination = new List<bool>();
        var bestValue = int.MinValue;

        var n = items.Count;
        for (var i = 0; i < (int) Math.Pow(2, n); i++)
        {
            var solution = Enumerable.Range(0, n).Select(j => (i >> j & 1) == 1).ToList();
            var (size, value) = CalculateTotalSizeAndValue(items, solution);
            if (size > capacity || value <= bestValue) continue;
            bestCombination = solution; 
            (_, bestValue) = (size, value);
        }

        return bestCombination;
    }

    public static Tuple<int, int> CalculateTotalSizeAndValue(IList<Item> data, IEnumerable<bool>? mask = null)
    {
        mask ??= Enumerable.Range(1, data.Count).Select(_ => true);
        var res = mask.Select((take, index) => take ? data[index] : null);
        var size = 0;
        var weight = 0;
        foreach (var item in res)
        {
            if (item is null) continue;
            size += item.Size;
            weight += item.Value;
        }

        return new Tuple<int, int>(size, weight);
    }
}