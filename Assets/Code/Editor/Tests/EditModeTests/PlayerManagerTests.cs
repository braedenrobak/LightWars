using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerManagerTests {

    private PlayerManager _gameManager;

    [SetUp]
    public void Setup()
    {
        _gameManager = new PlayerManager();
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

        Assert.AreEqual(4, _gameManager.GetPlayersCurrentEnergy(Players.PlayerOne));
        Assert.AreEqual(4, _gameManager.GetPlayersCurrentEnergy(Players.PlayerTwo));
    }

    [Test]
    public void FalseOnPlayerNotHavingEnoughEnergy()
    {
        Assert.False(_gameManager.PlayerCanCastEnergyOfCost(Players.PlayerOne, 5));
    }

    [Test]
    public void TrueOnPlayerHasEnoughEnergy()
    {
        Assert.True(_gameManager.PlayerCanCastEnergyOfCost(Players.PlayerOne, 3));
    }

    [Test]
    public void DamagesPlayer()
    {
        _gameManager.DamagePlayer(Players.PlayerTwo, 2);

        Assert.AreEqual(1, _gameManager.GetPlayersCurrentHealth(Players.PlayerTwo));
    }



}
