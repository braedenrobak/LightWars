using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerMain : MonoBehaviour {

    public GameObject _playerPrefab;
    public GameObject _energyPrefab;

    private GameManager _gameManager;

    private List<GameObject> _players;

    private BaseEnergySpawner _energySpawner;

	// Use this for initialization
	void Awake () {
        _energySpawner = new SinglePlayerEnergySpawner();
        _energySpawner.SetEnergyLoader(new FakeEnergyLoader());
        (_energySpawner as SinglePlayerEnergySpawner).energyPrefab = _energyPrefab;

        _energySpawner.LoadEnergies();

        _players = new List<GameObject>();

        for (int i = 0; i < 2; i++)
        {
            _players.Add(Instantiate(_playerPrefab));
        }

        _players[0].transform.position = new Vector3(0.0f, 3.0f, 0.0f);
        _players[1].transform.position = new Vector3(0.0f, -3.0f, 0.0f);

        PlayerView[] playerViews = { _players[0].GetComponent<PlayerView>(), _players[1].GetComponent<PlayerView>() };

        gameObject.AddComponent<PlayerViewOutputController>().SetPlayerViews(playerViews);

        _gameManager = gameObject.AddComponent<GameManager>();

        playerViews[0].InitializePlayerView(_gameManager, _energySpawner);
        playerViews[1].InitializePlayerView(_gameManager, _energySpawner);
	}
}
