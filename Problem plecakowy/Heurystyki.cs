namespace Problem_plecakowy;

public class Heurystyki
{
    public static List<bool> Random(int capacity, List<Item> items)
    {
        var possibleCombinations = new List<List<bool>>();
        BruteForce.generateAllBinaryStrings(items.Count, possibleCombinations);

        var rng = new Random();
        int size;
        int index;
        do
        {
            index = rng.Next(0, possibleCombinations.Count);
            (size, _) = BruteForce.calculateTotalValueAndWeight(items, possibleCombinations[index]);
        } while (size > capacity);

        return possibleCombinations[index];
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