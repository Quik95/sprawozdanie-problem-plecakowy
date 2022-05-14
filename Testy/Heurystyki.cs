using System.Collections.Generic;
using NUnit.Framework;
using Problem_plecakowy;

namespace Testy;

public class HeurystykiTest
{
    private static readonly List<Item> _testDataĆwiczenia = new()
    {
        new Item(5, 3),
        new Item(3, 4),
        new Item(2, 2),
        new Item(4, 6),
        new Item(3, 1),
    };

    private static readonly List<Item> _testDataPrezentacja = new()
    {
        new Item(1, 2),
        new Item(1, 1),
        new Item(2, 3),
        new Item(3, 7),
        new Item(3, 6),
    };

    [Test]
    public void Random1()
    {
        var solution = Heurystyki.Random(10, _testDataĆwiczenia);
    }

    [Test]
    public void Random2()
    {
        var solution = Heurystyki.Random(7, _testDataPrezentacja);
    }

    [Test]
    public void MinSize1()
    {
        var solution = Heurystyki.MinimumSize(10, _testDataĆwiczenia);
    }

    [Test]
    public void MinSize2()
    {
        var solution = Heurystyki.MinimumSize(7, _testDataPrezentacja);
    }
    [Test]
    public void MaxValue1()
    {
        var solution = Heurystyki.MaximumValue(10, _testDataĆwiczenia);
    }

    [Test]
    public void MaxValue2()
    {
        var solution = Heurystyki.MaximumValue(7, _testDataPrezentacja);
    } 
    [Test]
    public void Ratio1()
    {
        var solution = Heurystyki.Ratio(10, _testDataĆwiczenia);
        var reference = DynamicProgramming.FindSolution(10, _testDataĆwiczenia);

        Assert.AreEqual(reference, solution);
    }

    [Test]
    public void Ratio2()
    {
        var solution = Heurystyki.Ratio(7, _testDataPrezentacja);
    }
}