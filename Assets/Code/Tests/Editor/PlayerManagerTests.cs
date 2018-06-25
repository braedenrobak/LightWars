using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerManagerTests {

    private PlayerController _gameManager;

    [SetUp]
    public void Setup()
    {
        _gameManager = new PlayerController();
    }

    [TearDown]
    public void Teardown()
    {
        _gameManager = null;
    }

    [Test]
    public void GivesEnergyToPlayers()
    {
        _gameManager.AddEnergyToPlayers();

        Assert.AreEqual(4, _gameManager.GetPlayersCurrentEnergy(PlayerIds.PlayerOne));
        Assert.AreEqual(4, _gameManager.GetPlayersCurrentEnergy(PlayerIds.PlayerTwo));
    }

    [Test]
    public void FalseOnPlayerNotHavingEnoughEnergy()
    {
        Assert.False(_gameManager.PlayerCanCastEnergyOfCost(PlayerIds.PlayerOne, 5));
    }

    [Test]
    public void TrueOnPlayerHasEnoughEnergy()
    {
        Assert.True(_gameManager.PlayerCanCastEnergyOfCost(PlayerIds.PlayerOne, 3));
    }

    [Test]
    public void DamagesPlayer()
    {
        _gameManager.DamagePlayer(PlayerIds.PlayerTwo, 2);

        Assert.AreEqual(1, _gameManager.GetPlayersCurrentHealth(PlayerIds.PlayerTwo));
    }



}
