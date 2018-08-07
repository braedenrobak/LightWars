using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkRoundManagerVisual : NetworkBehaviour, IRoundManagerVisual
{
    public Text _announcerText;

    public void SetAnnouncerText(string text)
    {
        _announcerText.text = text;
    }

	private bool _roundVisualFinished = false;

    private int _currentRound = 1;

	public void EndRound(int winner)
    {
        if (!isServer)
            return;
        RpcStartRoundVisual(winner);
    }

    [ClientRpc]
    private void RpcStartRoundVisual(int winner)
    {
        _roundVisualFinished = false;
        _currentRound++;

        StartCoroutine(PlayInbetweenRoundVisual(winner));
    }

    public IEnumerator PlayInbetweenRoundVisual(int winnerId)
    {
        if (winnerId == Constants.LOCAL_PLAYER_ID)
        {
            SetAnnouncerText("You won the round!");
        }
        else
        {
            SetAnnouncerText("You lost the round you big fat loser!");
        }

        yield return new WaitForSeconds(2.0f);

        SetAnnouncerText("Round " + _currentRound + "!");

        yield return new WaitForSeconds(1.5f);

        SetAnnouncerText("3");

        yield return new WaitForSeconds(1.0f);

        SetAnnouncerText("2");

        yield return new WaitForSeconds(1.0f);

        SetAnnouncerText("1");

        yield return new WaitForSeconds(1.0f);

        SetAnnouncerText("Go");
        _roundVisualFinished = true;

        yield return new WaitForSeconds(0.5f);

        SetAnnouncerText("");
    }

    public void StartGame()
    {
        if (!isServer)
            return;

        RpcStartGame();
    }

    [ClientRpc]
    private void RpcStartGame()
    {
        _roundVisualFinished = false;
        StartCoroutine(BegginningGameVisual());
    }

    public IEnumerator BegginningGameVisual()
    {
        SetAnnouncerText("Welcome to the game!");

        yield return new WaitForSeconds(2.0f);

        SetAnnouncerText("Round " + _currentRound + "!");

        yield return new WaitForSeconds(1.0f);

        yield return new WaitForSeconds(1.5f);

        SetAnnouncerText("3");

        yield return new WaitForSeconds(1.0f);

        SetAnnouncerText("2");

        yield return new WaitForSeconds(1.0f);

        SetAnnouncerText("1");

        yield return new WaitForSeconds(1.0f);

        SetAnnouncerText("Go");
        _roundVisualFinished = true;

        yield return new WaitForSeconds(0.5f);

        SetAnnouncerText("");
    }



    public bool RoundVisualHasFinished()
    {
        return _roundVisualFinished;
    }
}
