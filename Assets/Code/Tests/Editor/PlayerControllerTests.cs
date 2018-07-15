using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerControllerTests {

    private PlayerManager _playerManager;

    [SetUp]
    public void Setup()
    {
        _playerManager = new PlayerManager();
    }

    [TearDown]
    public void Teardown()
    {
        _playerManager = null;
    }

    [Test]
    public void GivesEnergyToPlayers()
    {
        _playerManager.AddEnergyToPlayers();

        Assert.AreEqual(4, _playerManager.GetPlayersCurrentEnergy(Constants.PLAYER_ONE));
        Assert.AreEqual(4, _playerManager.GetPlayersCurrentEnergy(Constants.PLAYER_TWO));
    }

    [Test]
    public void FalseOnPlayerNotHavingEnoughEnergy()
    {
        Assert.False(_playerManager.PlayerCanCastEnergyOfCost(Constants.PLAYER_ONE, 5));
    }

    [Test]
    public void TrueOnPlayerHasEnoughEnergy()
    {
        Assert.True(_playerManager.PlayerCanCastEnergyOfCost(Constants.PLAYER_ONE, 3));
    }

    [Test]
    public void DamagesPlayer()
    {
        _playerManager.DamagePlayer(Constants.PLAYER_ONE, 2);

        Assert.AreEqual(1, _playerManager.GetPlayersCurrentHealth(Constants.PLAYER_ONE));
    }



}
