using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public PlayerIds id;
    public int health;
    public int energy;

    public PlayerData()
    {}

    public PlayerData(int id, int health, int energy)
    {
        this.id = (PlayerIds)id;
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

        _gameViewOutputController.UpdatePlayerView(ConvertToPlayerData(PlayerIds.PlayerOne));
        _gameViewOutputController.UpdatePlayerView(ConvertToPlayerData(PlayerIds.PlayerTwo));
	}
	
	// Update is called once per frame
	void Update () {
        _gameTimer += Time.deltaTime;

        if (_gameTimer >= CreationTime)
        {
            _playerManager.AddEnergyToPlayers();

            _gameViewOutputController.UpdatePlayerView(ConvertToPlayerData(PlayerIds.PlayerOne));
            _gameViewOutputController.UpdatePlayerView(ConvertToPlayerData(PlayerIds.PlayerTwo));
            _gameTimer = 0.0f;
        }

        if(_playerManager.GetPlayersCurrentHealth(PlayerIds.PlayerOne) <= 0)
        {
            _gameViewOutputController.GameOverWithWinner(PlayerIds.PlayerTwo);
        }
        else if(_playerManager.GetPlayersCurrentHealth(PlayerIds.PlayerTwo) <= 0)
        {
            _gameViewOutputController.GameOverWithWinner((int)PlayerIds.PlayerOne);
        }
	}

    public void PlayerHit(PlayerData player, EnergyData energy)
    {
        _playerManager.DamagePlayer(player.id, energy.damage);

        _gameViewOutputController.DisplayPlayerHit(player.id, energy.damage);

        _gameViewOutputController.UpdatePlayerView(ConvertToPlayerData((PlayerIds)player.id));
    }

    private PlayerData ConvertToPlayerData(PlayerIds player)
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
