namespace Problem_plecakowy;

public class BruteForce
{
    public static IEnumerable<bool> FindSolution(int capacity, IList<Item> items)
    {
        var combination = new List<List<bool>>();
        generateAllBinaryStrings(items.Count, combination);

        return FindBestSolution(combination, items, capacity);
    }

    public static IEnumerable<bool> FindBestSolution(List<List<bool>> solutions, IList<Item> items, int capacity)
    {
        var bestCombination = new List<bool>();
        var bestValue = Int32.MinValue;
        
        foreach (var solution in solutions)
        {
            var (size, value) = calculateTotalValueAndWeight(items, solution);
            if (size > capacity || value <= bestValue) continue;
            bestCombination = solution; 
            (_, bestValue) = (size, value);
        }

        return bestCombination;
    }

    public static Tuple<int, int> calculateTotalValueAndWeight(IList<Item> data, IEnumerable<bool>? mask = null)
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
    
    public static void generateAllBinaryStrings(int n, ICollection<List<bool>> res, List<bool>? arr = null, int i = 0)
    {
        while (true)
        {
            arr ??= new List<bool>(new bool[n]);

            if (i == n)
            {
                res.Add(arr);
                return;
            }

            arr[i] = false;
            generateAllBinaryStrings(n, res, new List<bool>(arr), i + 1);

            arr[i] = true;
            i += 1;
        }
    }
}