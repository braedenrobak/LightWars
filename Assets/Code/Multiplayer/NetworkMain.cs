using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMain : MonoBehaviour {

    public GameObject energyPrefab;
    public GameObject spawnPointPrefab;

    private GameManager _gameManager;
    private BaseEnergySpawner _energySpawner;
    private BaseSpawnPointSpawner _spawnPointSpawner;

    private NetworkPlayerOutputController _networkPlayerOutputController;
    private List<NetworkPlayerInput> _networkPlayerInputs;

    private int _currentPlayerId = 0;

	// Use this for initialization
	void Start () {
        _networkPlayerInputs = new List<NetworkPlayerInput>();

        _energySpawner = new NetworkEnergySpawner();
        (_energySpawner as NetworkEnergySpawner).energyPrefab = energyPrefab;

        _spawnPointSpawner = new NetworkSpawnPointSpawner();
        _spawnPointSpawner.SetSpawnPointLoader(new FakeSpawnPointLoader());
        (_spawnPointSpawner as NetworkSpawnPointSpawner).spawnPointPrefab = spawnPointPrefab;


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
            (_spawnPointSpawner as NetworkSpawnPointSpawner).SetNetworkPlayerInputs(_networkPlayerInputs);
            _spawnPointSpawner.LoadSpawnPoints();
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

        _networkPlayerInputs.Add(playerInput);

        _currentPlayerId++;
    }

    private bool GameHasStarted()
    {
        return _currentPlayerId == 2;
    }

}
