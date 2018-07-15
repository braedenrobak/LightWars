using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerControllerTests {

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

        Assert.AreEqual(4, _gameManager.GetPlayersCurrentEnergy(Constants.PLAYER_ONE));
        Assert.AreEqual(4, _gameManager.GetPlayersCurrentEnergy(Constants.PLAYER_TWO));
    }

    [Test]
    public void FalseOnPlayerNotHavingEnoughEnergy()
    {
        Assert.False(_gameManager.PlayerCanCastEnergyOfCost(Constants.PLAYER_ONE, 5));
    }

    [Test]
    public void TrueOnPlayerHasEnoughEnergy()
    {
        Assert.True(_gameManager.PlayerCanCastEnergyOfCost(Constants.PLAYER_ONE, 3));
    }

    [Test]
    public void DamagesPlayer()
    {
        _gameManager.DamagePlayer(Constants.PLAYER_ONE, 2);

        Assert.AreEqual(1, _gameManager.GetPlayersCurrentHealth(Constants.PLAYER_ONE));
    }



}
