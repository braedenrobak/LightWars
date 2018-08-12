using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkMain : NetworkBehaviour {

    public GameObject energyPrefab;
    public GameObject spawnPointPrefab;
    public GameObject roundManagerVisualPrefab;
    public GameObject rematchScreenPrefab;

    private GameManager _gameManager;
    private RoundManager _roundManager;
    private BaseEnergySpawner _energySpawner;
    private BaseSpawnPointSpawner _spawnPointSpawner;

    private NetworkPlayerOutputController _networkPlayerOutputController;
    private List<NetworkPlayerInput> _networkPlayerInputs;

    private int _currentPlayerId = 0;

	public void Update()
    {
        if(!_gameManager.enabled && GameHasStarted())
        {
            _spawnPointSpawner.LoadSpawnPoints();
            _energySpawner.LoadEnergies();

            // If you are the server spawn the visual on the clients
            if (isServer)
            {
                GameObject roundManagerVisual = Instantiate(roundManagerVisualPrefab, Vector3.zero, Quaternion.identity);
                NetworkServer.Spawn(roundManagerVisual);

                GameObject rematchScreen = Instantiate(rematchScreenPrefab, Vector3.zero, Quaternion.identity);
                NetworkServer.Spawn(rematchScreen);
            }

            // Find the spawned visual and connect it to the round manager
            GameObject serverSpawnedRoundManager = GameObject.Find("NetworkRoundManagerVisual(Clone)");
            _roundManager.SetVisual(serverSpawnedRoundManager.GetComponent<IRoundManagerVisual>());

            GameObject serverSpawnedRematchScreen = GameObject.Find("RematchScreen(Clone)");
            serverSpawnedRematchScreen.GetComponent<NetworkRematchScreen>().networkMain = this;

            _gameManager.StartGame();
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

    public void Setup()
    {
        _networkPlayerInputs = new List<NetworkPlayerInput>();

        _energySpawner = new NetworkEnergySpawner();
        _energySpawner.SetEnergyLoader(new NetworkTestEnergyLoader());
        (_energySpawner as NetworkEnergySpawner).energyPrefab = energyPrefab;

        _spawnPointSpawner = new NetworkSpawnPointSpawner();
        _spawnPointSpawner.SetSpawnPointLoader(new NetworkTestSpawnPointLoader());
        (_spawnPointSpawner as NetworkSpawnPointSpawner).spawnPointPrefab = spawnPointPrefab;

        // Create NetworkOutput 
        _networkPlayerOutputController = gameObject.AddComponent<NetworkPlayerOutputController>();

        // Create Game Manager
        _gameManager = gameObject.AddComponent<GameManager>();
        _gameManager.enabled = false;

        // Create RoundManager
        _roundManager = new RoundManager(3);

        _gameManager.SetRoundManager(_roundManager);

        _currentPlayerId = 0;
    }


    public void Rematch()
    {
        _roundManager = new RoundManager(3);
        GameObject serverSpawnedRoundManager = GameObject.Find("NetworkRoundManagerVisual(Clone)");
        _roundManager.SetVisual(serverSpawnedRoundManager.GetComponent<IRoundManagerVisual>());
        _gameManager.SetRoundManager(_roundManager);
        _gameManager.StartGame();
    }

	public void Reset()
	{
        _currentPlayerId = 0;

        _networkPlayerInputs.Clear();

        _spawnPointSpawner = null;
        _energySpawner = null;

        Destroy(_gameManager);
        Destroy(_networkPlayerOutputController);
	}

    public void CloseMatch()
    {
        if (!isServer)
            return;
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StopClient();
        Debug.LogError("Closing the match!");
    }

    private bool GameHasStarted()
    {
        return _currentPlayerId == 2;
    }


}
