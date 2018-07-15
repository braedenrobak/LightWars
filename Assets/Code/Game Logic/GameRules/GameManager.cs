using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerData
{
    public int id;
    public int health;
    public int energy;

    public PlayerData()
    {}

    public PlayerData(int id, int health, int energy)
    {
        this.id = id;
        this.health = health;
        this.energy = energy;
    }

}

public class EnergyData
{
    public int energyType;
    public int damage;
}

public class GameManager : MonoBehaviour {

    public float CreationTime = 3.0f;

    private PlayerManager _playerManager;

    private float _gameTimer;

    private IPlayerViewOutputController _playerViewOutputController;

    // Use this for initialization
    void Awake()
    {
        _playerManager = new PlayerManager();
        _gameTimer = 0.0f;
        CreationTime = 3.0f;
        _playerViewOutputController = gameObject.GetComponent<IPlayerViewOutputController>();

        _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData(Constants.PLAYER_ONE));
        _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData(1));
    }
	
	// Update is called once per frame
	void Update () {
        _gameTimer += Time.deltaTime;

        if (_gameTimer >= CreationTime)
        {
            _playerManager.AddEnergyToPlayers();

            _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData(Constants.PLAYER_ONE));
            _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData(Constants.PLAYER_TWO));
            _gameTimer = 0.0f;
        }

        if(_playerManager.GetPlayersCurrentHealth(Constants.PLAYER_ONE) <= 0)
        {
            _playerViewOutputController.GameOverWithWinner(Constants.PLAYER_TWO);
        }
        else if(_playerManager.GetPlayersCurrentHealth(Constants.PLAYER_TWO) <= 0)
        {
            _playerViewOutputController.GameOverWithWinner(Constants.PLAYER_ONE);
        }
	}

    public void PlayerHit(PlayerData player, EnergyData energy)
    {
        _playerManager.DamagePlayer(player.id, energy.damage);

        _playerViewOutputController.DisplayPlayerHit(player.id, energy.damage);

        _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData(player.id));
    }

    public bool PlayerCastEnergy(int playerId, int energyCost)
    {
        if(_playerManager.PlayerCanCastEnergyOfCost(playerId, energyCost))
        {
            _playerManager.PlayerCastsEnergy(playerId, energyCost);
            _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData(playerId));
            return true;
        }

        return false;
    }

    private PlayerData ConvertToPlayerData(int player)
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
