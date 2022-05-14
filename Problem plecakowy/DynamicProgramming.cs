namespace Problem_plecakowy;

public class DynamicProgramming
{
    public static List<bool> FindSolution(int capacity, List<Item> items)
    {
        var decisionMatrix = Enumerable.Range(1, items.Count + 1).Select(_ => new List<int>(new int[capacity + 1]))
            .ToList();

        for (var item = 1; item < decisionMatrix.Count; item++)
        {
            for (var capacityLeft = 1; capacityLeft < decisionMatrix.First().Count; capacityLeft++)
            {
                var currentItem = items[item-1];
                if (currentItem.Size > capacityLeft)
                {
                    decisionMatrix[item][capacityLeft] = decisionMatrix[item - 1][capacityLeft];
                    continue;
                }

                decisionMatrix[item][capacityLeft] = Math.Max(
                    decisionMatrix[item - 1][capacityLeft],
                    decisionMatrix[item - 1][capacityLeft - currentItem.Size] + currentItem.Value
                );
            }
        }

        var solution = new List<bool>(new bool[items.Count]);
        var row = items.Count;
        var column = capacity;

        for (var i = solution.Count - 1; i >= 0; i--)
        {
            if (decisionMatrix[row][column] == decisionMatrix[row - 1][column])
            {
                solution[i] = false;
            }
            else
            {
                solution[i] = true;
                column -= items[row-1].Size;
            }

            row--;
        }

        return solution;
    }
}