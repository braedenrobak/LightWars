using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MockPlayModeRoundManagerVisual : MonoBehaviour, IRoundManagerVisual
{
    private bool _roundIsReady = false;

    public bool gameIsOver = false;

    public int winnerId = -1;

    public void EndRound(int winner)
    {
        StartCoroutine(FakeRoundEndVisual());
    }

    public bool RoundVisualHasFinished()
    {
        return _roundIsReady;
    }

    public IEnumerator FakeRoundEndVisual()
    {
        yield return new WaitForSeconds(1.5f);

        _roundIsReady = true;
    }

    public void StartGame()
    {
        // Stubbed
    }

    public void EndGame(int winner)
    {
        gameIsOver = true;

        winnerId = winner;
    }
}

public class RoundManagerPlayModeTest {
    
    private RoundManager _roundManager;

    private GameObject _roundManagerVisual;

    [SetUp]
    public void Setup()
    {
        _roundManagerVisual = new GameObject("Round Manager Visual");
        IRoundManagerVisual roundManagerVisual = _roundManagerVisual.AddComponent<MockPlayModeRoundManagerVisual>();

        _roundManager = new RoundManager(3);
        _roundManager.SetVisual(roundManagerVisual);
    }

    [TearDown]
    public void TearDown()
    {
        _roundManager = null;

        GameObject.Destroy(_roundManagerVisual);
    }

    [UnityTest]
    public IEnumerator InformThatRoundIsReadyOnceVisualIsDone()
    {
        _roundManager.EndRound(0);

        Assert.IsFalse(_roundManager.RoundIsReady());

        yield return new WaitForSeconds(2.0f);

        Assert.IsTrue(_roundManager.RoundIsReady());
    }

    [UnityTest]
    public IEnumerator GameOverOnLossOfBestOf()
    {
        _roundManager.EndRound(0);

        yield return new WaitForSeconds(2.0f);

        _roundManager.EndRound(0);

    }
}
