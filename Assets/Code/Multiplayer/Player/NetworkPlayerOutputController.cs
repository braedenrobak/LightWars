using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerOutputController : NetworkBehaviour, IPlayerViewOutputController
{
    private List<NetworkPlayerView> _networkPlayerViews;

    public void Awake()
    {
        _networkPlayerViews = new List<NetworkPlayerView>();
    }

    public void AddNetworkPlayerView(NetworkPlayerView playerView)
    {
        if (!isServer)
            return;
        
        _networkPlayerViews.Add(playerView);
    }

    public void DisplayPlayerHit(int playerId, int damage)
    {
        // Run damage anim
    }

    public void GameOverWithWinner(int playerId)
    {
        // Load winner stuff
    }

    public void UpdatePlayerView(PlayerData playerData)
    {
        if (!isServer)
            return;
        
        if (GameIsLoaded())
        {
            _networkPlayerViews[playerData.id].UpdateEnergy(playerData.energy);
            _networkPlayerViews[playerData.id].UpdateHealth(playerData.health);
        }
    }

    private bool GameIsLoaded()
    {
        return _networkPlayerViews.Count == 2;
    }

}
