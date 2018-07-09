using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMain : MonoBehaviour {

    public GameObject energyPrefab;

    private GameManager _gameManager;
    private BaseEnergySpawner _energySpawner;

    private NetworkPlayerOutputController _networkPlayerOutputController;

    private int _currentPlayerId = 0;

	// Use this for initialization
	void Start () {
        _energySpawner = new NetworkEnergySpawner();
        (_energySpawner as NetworkEnergySpawner).energyPrefab = energyPrefab;


        // Create Game Manager
        _gameManager = gameObject.AddComponent<GameManager>();
        _gameManager.enabled = false;
        // Create NetworkOutput 
        _networkPlayerOutputController = GetComponent<NetworkPlayerOutputController>();

        _currentPlayerId = 0;
	}

    public void Update()
    {
        if(!_gameManager.enabled && GameHasStarted())
        {
            _gameManager.enabled = true;
        }
    }

    public void AddPlayer(GameObject player)
    {
        NetworkPlayerInput playerInput = player.GetComponent<NetworkPlayerInput>();

        playerInput.SetGameManager(_gameManager);
        playerInput.SetEnergySpawner(_energySpawner);
        playerInput.SetPlayerId(_currentPlayerId);

        // Pass playerView to output w/ currentPlayerIndex
        _networkPlayerOutputController.AddNetworkPlayerView(player.GetComponent<NetworkPlayerView>());

        _currentPlayerId++;
    }

    private bool GameHasStarted()
    {
        return _currentPlayerId == 2;
    }

}
