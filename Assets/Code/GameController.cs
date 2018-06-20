using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public Players id;
    public int health;
    public int energy;

    public PlayerData()
    {}

    public PlayerData(int id, int health, int energy)
    {
        this.id = (Players)id;
        this.health = health;
        this.energy = energy;
    }

}

public class EnergyData
{
    public int energyType;
    public int damage;
}

public class GameController : MonoBehaviour {

    public float CreationTime = 3.0f;

    private PlayerManager _playerManager;

    private float _gameTimer;

    private float _energyCreationTime;

    private IGameViewOutputController _gameViewOutputController;

    private bool _gameOver;

	// Use this for initialization
	void Start () {
        _playerManager = new PlayerManager();
        _gameTimer = 0.0f;
        _energyCreationTime = CreationTime;
        _gameOver = false;
        _gameViewOutputController = GetComponent<IGameViewOutputController>();

        _gameViewOutputController.UpdatePlayerView(ConvertToPlayerData(Players.PlayerOne));
        _gameViewOutputController.UpdatePlayerView(ConvertToPlayerData(Players.PlayerTwo));
	}
	
	// Update is called once per frame
	void Update () {
        _gameTimer += Time.deltaTime;

        if (_gameTimer >= CreationTime)
        {
            _playerManager.AddEnergyToPlayers();

            _gameViewOutputController.UpdatePlayerView(ConvertToPlayerData(Players.PlayerOne));
            _gameViewOutputController.UpdatePlayerView(ConvertToPlayerData(Players.PlayerTwo));
            _gameTimer = 0.0f;
        }

        if(_playerManager.GetPlayersCurrentHealth(Players.PlayerOne) <= 0)
        {
            _gameViewOutputController.GameOverWithWinner(Players.PlayerTwo);
        }
        else if(_playerManager.GetPlayersCurrentHealth(Players.PlayerTwo) <= 0)
        {
            _gameViewOutputController.GameOverWithWinner((int)Players.PlayerOne);
        }
	}

    public void PlayerHit(PlayerData player, EnergyData energy)
    {
        _playerManager.DamagePlayer(player.id, energy.damage);

        _gameViewOutputController.DisplayPlayerHit(player.id, energy.damage);

        _gameViewOutputController.UpdatePlayerView(ConvertToPlayerData((Players)player.id));
    }

    private PlayerData ConvertToPlayerData(Players player)
    {
        PlayerData convertedPlayerData = new PlayerData
        {
            id = player,

            health = _playerManager.GetPlayersCurrentHealth(player),
            energy = _playerManager.GetPlayersCurrentEnergy(player)
        };

        return convertedPlayerData;
    }
}
