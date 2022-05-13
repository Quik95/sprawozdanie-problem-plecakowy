using System.Collections.Generic;
using NUnit.Framework;
using Problem_plecakowy;

namespace Testy;

public class Tests
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
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PrzykładZĆwiczeń()
    {
        var solution = BruteForce.FindSolution(10, _testDataĆwiczenia);
        Assert.AreEqual(
            new List<int> {0, 1, 1, 1, 0},
            solution
        );
    }
    [Test]
    public void PrzykładZPrezentacji()
    {
        var solution = BruteForce.FindSolution(7, _testDataPrezentacja);
        Assert.AreEqual(
            new List<int> {1, 0, 0, 1, 1},
            solution
        );
    } 
}