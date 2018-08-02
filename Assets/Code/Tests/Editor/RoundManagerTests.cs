using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class RoundManagerTests{

    private RoundManager _roundManager;

    [SetUp]
    public void Setup()
    {
        _roundManager = new RoundManager(5);
    }

    [TearDown]
    public void TearDown()
    {
        _roundManager = null;
    }

    [Test]
    public void StoreCorrectNumberOfRounds()
    {
        Assert.AreEqual(5, _roundManager.NumberOfRounds());
    }

    [Test]
    public void CreatesBestOfBasedOnNumberOfRounds()
    {
        Assert.AreEqual(3, _roundManager.GetBestOfRoundCount());

        _roundManager = new RoundManager(6);

        Assert.AreEqual(4, _roundManager.GetBestOfRoundCount());
    }

    [Test]
    public void TracksCurrentRound()
    {
        Assert.AreEqual(1, _roundManager.GetCurrentRound());

        _roundManager.EndRound(42);

        Assert.AreEqual(2, _roundManager.GetCurrentRound());
    }

    [Test]
    public void StoresWinnerWithRoundTheyHaveWon()
    {
        int winnerId = 42;

        _roundManager.EndRound(winnerId);

        Assert.AreEqual(42, _roundManager.GetRoundWinners()[0]);
    }

    [Test]
    public void ReturnsWinnerIfBestOfIsReached()
    {
        _roundManager.EndRound(1);

        Assert.IsFalse(_roundManager.HasWinner());

        _roundManager.EndRound(1);

        Assert.IsFalse(_roundManager.HasWinner());

        _roundManager.EndRound(1);

        Assert.IsTrue(_roundManager.HasWinner());
        Assert.AreEqual(1, _roundManager.GetWinner());




        _roundManager = new RoundManager(3);

        _roundManager.EndRound(0);

        Assert.IsFalse(_roundManager.HasWinner());

        _roundManager.EndRound(1);

        Assert.IsFalse(_roundManager.HasWinner());

        _roundManager.EndRound(0);

        Assert.IsTrue(_roundManager.HasWinner());
        Assert.AreEqual(0, _roundManager.GetWinner());
    }

    [Test]
    public void SuddenDeathOnTieGameWithEvenNumberOfRounds()
    {
        _roundManager = new RoundManager(2);

        _roundManager.EndRound(0);

        _roundManager.EndRound(1);

        Assert.IsFalse(_roundManager.HasWinner());

        _roundManager.EndRound(0);

        Assert.IsTrue(_roundManager.HasWinner());
        Assert.AreEqual(0, _roundManager.GetWinner());
    }
}
