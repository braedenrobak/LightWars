using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerOutputController : NetworkBehaviour, IPlayerViewOutputController
{
    // ** TODO : DECIDE IF IT NEEDS TO BE DECIED IF ITS SERVER OR NOT BEFORE UPDATING
    public void DisplayPlayerHit(PlayerIds playerId, int damage)
    {
       // Play damage animation at the playerId's position
    }

    public void GameOverWithWinner(PlayerIds playerId)
    {
        // Load winner stuff
    }

    public void UpdatePlayerView(PlayerData playerData)
    {
        // Send update to the PlayerView
    }
}
