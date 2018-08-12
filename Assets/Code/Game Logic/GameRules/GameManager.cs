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

    public float CreationTime = 1.5f;

    private PlayerManager _playerManager;

    private RoundManager _roundManager;

    private float _gameTimer;

    private IPlayerViewOutputController _playerViewOutputController;

    // Use this for initialization
    void Awake()
    {
        CreationTime = 1.5f;
        _playerViewOutputController = gameObject.GetComponent<IPlayerViewOutputController>();
    }

    public void SetRoundManager(RoundManager roundManager)
    {
        _roundManager = roundManager;
    }

    public void StartGame()
    {
        _roundManager.StartGame();
        StartRound();
    }

	// Update is called once per frame
	void Update () {

        if (_roundManager.RoundIsReady())
        {
            // Progress GM specific time
            _gameTimer += Time.deltaTime;

            // Update player energies
            if (_gameTimer >= CreationTime)
            {
                _playerManager.AddEnergyToPlayers();

                _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData(Constants.PLAYER_ONE));
                _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData(Constants.PLAYER_TWO));
                _gameTimer = 0.0f;
            }


            // Check for game end
            if (_playerManager.GetPlayersCurrentHealth(Constants.PLAYER_ONE) <= 0)
            {
                _roundManager.EndRound(Constants.PLAYER_TWO);

                CheckForWinner();
            }
            else if (_playerManager.GetPlayersCurrentHealth(Constants.PLAYER_TWO) <= 0)
            {
                _roundManager.EndRound(Constants.PLAYER_ONE);

                CheckForWinner();
            }
        }
	}

    private void CheckForWinner()
    {
        if (_roundManager.HasWinner())
        {
            _roundManager.EndGame(_roundManager.GetWinner());
        }
        else
        {
            StartRound();
        }
    }


    public void StartRound()
    {
        _playerManager = new PlayerManager();
        _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData(Constants.PLAYER_ONE));
        _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData(Constants.PLAYER_TWO));
        _gameTimer = 0.0f;
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
