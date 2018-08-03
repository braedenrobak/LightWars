using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkRoundManagerVisual : NetworkBehaviour, IRoundManagerVisual
{
    
    public Text _announcerText;

    [SyncVar(hook = "UpdateAnnouncerText")]
    private string _currentAnnouncerText;

	public void UpdateAnnouncerText(string currentAnnouncerText)
    {
        _announcerText.text = currentAnnouncerText;
    }

	private bool _roundVisualFinished = true;

    private int _currentRound = 1;

	public void EndRound(int winner)
    {
        _roundVisualFinished = false;
        _currentRound++;
        StartCoroutine(PlayInbetweenRoundVisual(winner));
    }

    public IEnumerator PlayInbetweenRoundVisual(int winnerId)
    {
        if(winnerId == Constants.LOCAL_PLAYER_ID)
        {
            UpdateAnnouncerText("You won the round!");
            //_announcerText.text = "You won the round!";
        }
        else
        {
            UpdateAnnouncerText("You lost the round you big fat loser!");
            //_announcerText.text = "You lost the round you big fat loser!";
        }

        yield return new WaitForSeconds(2.0f);

        _announcerText.text = "Round " + _currentRound + "!";

        yield return new WaitForSeconds(1.5f);

        _announcerText.text = "3";

        yield return new WaitForSeconds(1.0f);

        _announcerText.text = "2";

        yield return new WaitForSeconds(1.0f);

        _announcerText.text = "1";

        yield return new WaitForSeconds(1.0f);

        _announcerText.text = "Go";
        _roundVisualFinished = true;

        yield return new WaitForSeconds(0.5f);

        _announcerText.text = "";
    }

    public bool RoundVisualHasFinished()
    {
        return _roundVisualFinished;
    }
}
