using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int id;
    public int health;
    public int energy;

    public PlayerData(int id, int health, int energy)
    {
        this.id = id;
        this.health = health;
        this.energy = energy;
    }

}

public class GameController : MonoBehaviour {

    public float CreationTime = 3.0f;

    private PlayerManager _playerManager;

    private PlayerData _playerOne;

    private PlayerData _playerTwo;

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

        /** TODO:  FIX TO INITIALIZE IT TO PLAYERMANAGER NUMBERS **/
        _playerOne = new PlayerData(0, 3, 3);
        _playerTwo = new PlayerData(1, 3, 3);
	}
	
	// Update is called once per frame
	void Update () {
        _gameTimer += Time.deltaTime;

        if (_gameTimer >= CreationTime)
        {
            Debug.Log("Entered the energy loop");
            _playerManager.AddEnergyToPlayers();
            // Update view with new player energies
            _playerOne.energy = _playerManager.GetPlayersCurrentEnergy(Players.PlayerOne);
            _playerTwo.energy = _playerManager.GetPlayersCurrentEnergy(Players.PlayerTwo);

            _gameViewOutputController.UpdatePlayerView(_playerOne);
            _gameViewOutputController.UpdatePlayerView(_playerTwo);
            _gameTimer = 0.0f;
        }

        if(_playerManager.GetPlayersCurrentHealth(Players.PlayerOne) == 0)
        {
            _gameViewOutputController.GameOverWithWinner((int)Players.PlayerTwo);
        }
        else if(_playerManager.GetPlayersCurrentHealth(Players.PlayerTwo) == 0)
        {
            _gameViewOutputController.GameOverWithWinner((int)Players.PlayerOne);
        }
	}

    public void PlayerHit(GameObject player, GameObject energy)
    {
        Players currentPlayer = (Players)player.GetComponent<PlayerData>().id;
        //_playerManager.DamagePlayer(currentPlayer, energy.GetComponent<Energy>().damage);

        int currentEnergy = _playerManager.GetPlayersCurrentEnergy(currentPlayer);
        int currentHealth = _playerManager.GetPlayersCurrentHealth(currentPlayer);

        int currentPlayerId = player.GetComponent<PlayerData>().id;
       // _gameViewOutputController.DisplayPlayerHit(currentPlayerId, energy.GetComponent<Energy>().damage);
        _gameViewOutputController.UpdatePlayerView(new PlayerData(currentPlayerId, currentHealth, currentEnergy));
    }
}
