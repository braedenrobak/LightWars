using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MockGameViewOutput : MonoBehaviour, IGameViewOutputController
{
    public PlayerData playerOne = new PlayerData(0,0,0);
    public PlayerData playerTwo = new PlayerData(1,0,0);

    public bool wasHit = false;
    public PlayerIds hitPlayerId;

    public bool gameOverCalled = false;
    public PlayerIds playerIdOfWinner;

    public void DisplayPlayerHit(PlayerIds playerId, int damage)
    {
        wasHit = true;
        hitPlayerId = playerId;
    }

    public void GameOverWithWinner(PlayerIds playerId)
    {
        gameOverCalled = true;
        playerIdOfWinner = playerId;
    }

    public void UpdatePlayerView(PlayerData playerData)
    {
        if (playerData.id == 0)
        {
            playerOne = playerData;
        }
        else
        {
            playerTwo = playerData;
        }
    }
}


public class GameControllerTests
{

    private GameObject _gameControllerObject;

    private GameController _gameController;

    private MockGameViewOutput _mockGameViewOutput;


    [SetUp]
    public void Setup()
    {
        _gameControllerObject = new GameObject("Game Controller");
        _mockGameViewOutput = _gameControllerObject.AddComponent<MockGameViewOutput>();
        _gameController = _gameControllerObject.AddComponent<GameController>();
    }

    [TearDown]
    public void Teardown()
    {
        _gameController = null;
        Object.DestroyImmediate(_gameControllerObject);
    }

    [UnityTest]
    public IEnumerator InitializedPlayerDataPassedToViewOnStartUp()
    {
        PlayerData playerDataToTest = _mockGameViewOutput.playerOne;
        Assert.AreEqual(PlayerIds.PlayerOne, playerDataToTest.id);
        Assert.AreEqual(3, playerDataToTest.health);
        Assert.AreEqual(3, playerDataToTest.energy);

        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerTakesDamageGivenPlayerObjectAndEnergyObject()
    {
        _gameController.PlayerHit(_mockGameViewOutput.playerOne, new EnergyData { energyType = 0, damage = 1} );

        Assert.True(_mockGameViewOutput.wasHit);
        Assert.AreEqual(PlayerIds.PlayerOne, _mockGameViewOutput.hitPlayerId);
        Assert.AreEqual(2, _mockGameViewOutput.playerOne.health);

        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayersGainEnergyAfterSpecifiedTime()
    {
        yield return new WaitForSeconds(4.0f);

        Assert.AreEqual(4, _mockGameViewOutput.playerOne.energy);
        Assert.AreEqual(4, _mockGameViewOutput.playerTwo.energy);

        yield return null;
    }

    [UnityTest]
    public IEnumerator GameOverOnPlayerDied()
    {
        _gameController.PlayerHit(_mockGameViewOutput.playerOne, new EnergyData { energyType = 0, damage = 4 });

        yield return new WaitForSeconds(2.0f);

        Assert.True(_mockGameViewOutput.gameOverCalled);
        Assert.AreEqual(PlayerIds.PlayerTwo, _mockGameViewOutput.playerIdOfWinner);

        yield return null;
    }
}