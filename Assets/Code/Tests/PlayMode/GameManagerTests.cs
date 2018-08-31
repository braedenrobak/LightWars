using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MockGameViewOutput : MonoBehaviour, IPlayerViewOutputController
{
    public PlayerData playerOne = new PlayerData(Constants.PLAYER_ONE,0,0);
    public PlayerData playerTwo = new PlayerData(Constants.PLAYER_TWO,0,0);

    public bool wasHit = false;
    public int hitPlayerId;

    public bool gameOverCalled = false;
    public int playerIdOfWinner;

    public void DisplayPlayerHit(int playerId, int damage)
    {
        wasHit = true;
        hitPlayerId = playerId;
    }

    public void GameOverWithWinner(int playerId)
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


public class GameManagerTests
{
    private GameObject _gameManagerObject;

    private GameManager _gameManager;

    private MockGameViewOutput _mockGameViewOutput;


    [SetUp]
    public void Setup()
    {
        _gameManagerObject = new GameObject("Game Controller");
        _mockGameViewOutput = _gameManagerObject.AddComponent<MockGameViewOutput>();
        _gameManager = _gameManagerObject.AddComponent<GameManager>();

        RoundManager roundManager = new RoundManager(3);
        roundManager.SetVisual(new NullRoundManagerVisual());
        _gameManager.SetRoundManager(roundManager);

        _gameManager.StartGame();
    }

    [TearDown]
    public void Teardown()
    {
        _gameManager = null;
        Object.DestroyImmediate(_gameManagerObject);
    }

    [UnityTest]
    public IEnumerator InitializedPlayerDataPassedToViewOnStartUp()
    {
        PlayerData playerDataToTest = _mockGameViewOutput.playerOne;
        Assert.AreEqual(Constants.PLAYER_ONE, playerDataToTest.id);
        Assert.AreEqual(3, playerDataToTest.health);
        Assert.AreEqual(3, playerDataToTest.energy);

        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerTakesDamageGivenPlayerObjectAndEnergyObject()
    {
        _gameManager.PlayerHit(_mockGameViewOutput.playerOne, new EnergyData { energyType = 0, damage = 1} );

        Assert.True(_mockGameViewOutput.wasHit);
        Assert.AreEqual(Constants.PLAYER_ONE, _mockGameViewOutput.hitPlayerId);
        Assert.AreEqual(2, _mockGameViewOutput.playerOne.health);

        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayersGainEnergyAfterSpecifiedTime()
    {
        yield return new WaitForSeconds(2.0f);

        Assert.AreEqual(4, _mockGameViewOutput.playerOne.energy);
        Assert.AreEqual(4, _mockGameViewOutput.playerTwo.energy);

        yield return null;
    }

    [UnityTest]
    public IEnumerator GameManagerStartsFreshRoundOnRoundOver()
    {

        _gameManager.PlayerHit(_mockGameViewOutput.playerOne, new EnergyData { energyType = 0, damage = 4 });

        yield return new WaitForSeconds(1.0f);

        Assert.AreEqual(3, _mockGameViewOutput.playerOne.health);
        Assert.AreEqual(3, _mockGameViewOutput.playerTwo.health);

        Assert.AreEqual(3, _mockGameViewOutput.playerOne.energy);
        Assert.AreEqual(3, _mockGameViewOutput.playerTwo.energy);

        yield return null;
    }
}