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

    public void DisplayPlayerHit(int playerId, int damage)
    {
        _playerViews[playerId].DisplayHit(damage);
    }

    public void GameOverWithWinner(int playerId)
    {
       // Open up winner winner light war dinner screen and display playerId as victorious
    }

    public void UpdatePlayerView(PlayerData playerData)
    {
        _playerViews[playerData.id].UpdateVisual(playerData);
    }
}
