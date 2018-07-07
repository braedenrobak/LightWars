using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewOutputController : MonoBehaviour, IPlayerViewOutputController
{
    private PlayerView[] _playerViews;

    public void SetPlayerViews(PlayerView[] playerViews)
    {
        _playerViews = playerViews;
    }

    public void DisplayPlayerHit(PlayerIds playerId, int damage)
    {
        _playerViews[(int)playerId].DisplayHit(damage);
    }

    public void GameOverWithWinner(PlayerIds playerId)
    {
       // Open up winner winner light war dinner screen and display playerId as victorious
    }

    public void UpdatePlayerView(PlayerData playerData)
    {
        _playerViews[(int)playerData.id].UpdateVisual(playerData);
    }
}
