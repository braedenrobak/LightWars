using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MockGameViewOutput : MonoBehaviour, IGameViewOutputController
{
    public PlayerData playerOne = new PlayerData(0,0,0);
    public PlayerData playerTwo = new PlayerData(0,0,0);

    public void DisplayPlayerHit(int playerId, int damage)
    {
        // Stubbed for testing purposes
    }

    public void GameOverWithWinner(int playerId)
    {
        // Stubbed for testing purposes
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
    public void PlayerTakesDamageGivenPlayerObjectAndEnergyObject()
    {

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
    public void GameOverOnPlayerDied()
    {

    }
}