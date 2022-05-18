using System.Collections.Generic;
using NUnit.Framework;
using Algorytmy;

namespace Testy;

public class BruteForceWithBacktrackingTests
{
    private static readonly List<Item> TestDataĆwiczenia = new()
    {
        new Item(5, 3),
        new Item(3, 4),
        new Item(2, 2),
        new Item(4, 6),
        new Item(3, 1),
    };

    private static readonly List<Item> TestDataPrezentacja = new()
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
        var solution = BruteForceWithBacktracking.FindSolution(10, TestDataĆwiczenia);
        Assert.AreEqual(
            new List<bool> {false, true, true, true, false},
            solution
        );
    }
    [Test]
    public void PrzykładZPrezentacji()
    {
        var solution = BruteForceWithBacktracking.FindSolution(7, TestDataPrezentacja);
        Assert.AreEqual(
            new List<bool> {true, false, false, true, true},
            solution
        );
    } 
}