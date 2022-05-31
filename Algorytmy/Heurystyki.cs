namespace Algorytmy;

public class Heurystyki
{
    private static readonly Random Rng = new();

    public static IEnumerable<bool> Random(int capacity, List<Item> items)
    {
        var res = Enumerable.Range(1, items.Count).Select(_ => false).ToList();
        int currentSize;
        int lastIdx;

        do
        {
            lastIdx = Rng.Next(0, res.Count);
            res[lastIdx] = true;
            (currentSize, _) = BruteForce.CalculateTotalSizeAndValue(items, res);
        } while (currentSize <= capacity);
        
        res[lastIdx] = false;

        return res;
    }

    public static IEnumerable<bool> MinimumSize(int capacity, List<Item> items)
    {
        var temp = items.Select((item, index) => new Tuple<Item, int>(item, index))
            .OrderBy(x => x.Item1.Size).ToList();
        var res = items.Select(_ => false).ToList();
        foreach (var (item, index) in temp)
        {
            if (capacity == 0)
                break;

            if (item.Size > capacity) continue;
            res[index] = true;
            capacity -= item.Size;

        }

        return res;
    }

    public static IEnumerable<bool> MaximumValue(int capacity, List<Item> items)
    {
         var temp = items.Select((item, index) => new Tuple<Item, int>(item, index))
             .OrderByDescending(x => x.Item1.Value).ToList();
         var res = items.Select(_ => false).ToList();
         foreach (var (item, index) in temp)
         {
             if (capacity == 0)
                 break;
 
             if (item.Size > capacity) continue;
             res[index] = true;
             capacity -= item.Size;
 
         }
 
         return res;       
    }
    public static IEnumerable<bool> Ratio(int capacity, List<Item> items)
        {
             var temp = items.Select((item, index) => new Tuple<Item, int>(item, index))
                 .OrderByDescending(x => x.Item1.Value / x.Item1.Size).ToList();
             var res = items.Select(_ => false).ToList();
             foreach (var (item, index) in temp)
             {
                 if (capacity == 0)
                     break;
     
                 if (item.Size > capacity) continue;
                 res[index] = true;
                 capacity -= item.Size;
     
             }
     
             return res;       
        }
}