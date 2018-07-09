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

public class GameManager : MonoBehaviour {

    public float CreationTime = 3.0f;

    private PlayerController _playerController;

    private float _gameTimer;

    private IPlayerViewOutputController _playerViewOutputController;

    // Use this for initialization
    void Awake()
    {
        _playerController = new PlayerController();
        _gameTimer = 0.0f;
        CreationTime = 3.0f;
        _playerViewOutputController = gameObject.GetComponent<IPlayerViewOutputController>();

        _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData(PlayerIds.PlayerOne));
        _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData(PlayerIds.PlayerTwo));
    }
	
	// Update is called once per frame
	void Update () {
        _gameTimer += Time.deltaTime;

        if (_gameTimer >= CreationTime)
        {
            _playerController.AddEnergyToPlayers();

            _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData(PlayerIds.PlayerOne));
            _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData(PlayerIds.PlayerTwo));
            _gameTimer = 0.0f;
        }

        if(_playerController.GetPlayersCurrentHealth(PlayerIds.PlayerOne) <= 0)
        {
            _playerViewOutputController.GameOverWithWinner(PlayerIds.PlayerTwo);
        }
        else if(_playerController.GetPlayersCurrentHealth(PlayerIds.PlayerTwo) <= 0)
        {
            _playerViewOutputController.GameOverWithWinner((int)PlayerIds.PlayerOne);
        }
	}

    public void PlayerHit(PlayerData player, EnergyData energy)
    {
        _playerController.DamagePlayer(player.id, energy.damage);

        _playerViewOutputController.DisplayPlayerHit(player.id, energy.damage);

        _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData((PlayerIds)player.id));
    }

    public bool PlayerCastEnergy(int playerId, int energyCost)
    {
        if(_playerController.PlayerCanCastEnergyOfCost((PlayerIds)playerId, energyCost))
        {
            _playerController.PlayerCastsEnergy(playerId, energyCost);
            _playerViewOutputController.UpdatePlayerView(ConvertToPlayerData((PlayerIds)playerId));
            return true;
        }

        return false;
    }

    private PlayerData ConvertToPlayerData(PlayerIds player)
    {
        PlayerData convertedPlayerData = new PlayerData
        {
            id = player,

            health = _playerController.GetPlayersCurrentHealth(player),
            energy = _playerController.GetPlayersCurrentEnergy(player)
        };

        return convertedPlayerData;
    }
}
