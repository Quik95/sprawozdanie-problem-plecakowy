namespace Algorytmy;

public record Item(int Size, int Value);

public static class RandomItemGenerator
{
    private const int SizeLowerBound = 10;
    private const int SizeUpperBound = 1000;
    
    private const int ValueLowerBound = 100;
    private const int ValueUpperBound = 10_000;

    private static readonly Random Rng = new();
    
    public static List<Item> Generate(int n)
    {
        return Enumerable.Range(1, n).Select(_ => new Item(Rng.Next(SizeLowerBound, SizeUpperBound + 1),
            Rng.Next(ValueLowerBound, ValueUpperBound + 1))).ToList();
    }

    public static int GetKnapsackCapacity(IEnumerable<Item> items, double b)
    {
        return (int)Math.Floor(items.Select(item => item.Size).Sum() * b);
    }
}