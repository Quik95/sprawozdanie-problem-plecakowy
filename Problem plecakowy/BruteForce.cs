namespace Problem_plecakowy;

public class BruteForce
{
    public static IEnumerable<int> FindSolution(int capacity, IList<Item> items)
    {
        var combination = new List<List<int>>();
        _generateAllBinaryStrings(items.Count, combination);

        var bestCombination = new List<int>();
        var bestValue = Int32.MinValue;

        foreach (var comb in combination.Skip(1))
        {
            var (size, value) = calculateTotalValueAndWeight(items, comb);
            if (size > capacity || value <= bestValue) continue;
            bestCombination = comb; 
            (_, bestValue) = (size, value);
        }

        return bestCombination;
    }

    public static Tuple<int, int> calculateTotalValueAndWeight(IList<Item> data, IEnumerable<int> mask)
    {
        var res = mask.Select((take, index) => take == 1 ? data[index] : null);
        var size = 0;
        var weight = 0;
        foreach (var item in res)
        {
            if (item is null) continue;
            size += item.Weight;
            weight += item.Value;
        }

        return new Tuple<int, int>(size, weight);
    }
    
    // Function to generate all binary strings
    private static void _generateAllBinaryStrings(int n, ICollection<List<int>> res, List<int>? arr = null, int i = 0)
    {
        while (true)
        {
            arr ??= new List<int>(new int[n]);

            if (i == n)
            {
                res.Add(arr);
                return;
            }

            arr[i] = 0;
            _generateAllBinaryStrings(n, res, new List<int>(arr), i + 1);

            arr[i] = 1;
            i += 1;
        }
    }
}