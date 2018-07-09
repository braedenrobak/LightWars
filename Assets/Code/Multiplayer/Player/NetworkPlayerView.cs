using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkPlayerView : NetworkBehaviour {

    public Text _healthText;
    public Text _energyText;
    public Text _nameText;

    [SyncVar(hook = "OnChangeHealth")]
    private int _health;

    [SyncVar(hook = "OnChangeEnergy")]
    private int _energy;

    [SyncVar(hook = "OnChangePlayerID")]
    private int _playerId;

    private GameManager _gameManager;
    private BaseEnergySpawner _energySpawner;

    // All about updating visual
    // This is where you connect the variables to be chagned across clients when changed

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void SetEnergySpawner(BaseEnergySpawner energySpawner)
    {
        _energySpawner = energySpawner;
    }


    public void SetPlayerId(int playerId)
    {
        _playerId = playerId;
    }

    // Set the non-local players view to the top of the screen
    public override void OnStartClient()
    {
        if (!isLocalPlayer)
        {
            transform.position = Constants.NON_LOCAL_PLAYER_POSITION;
            _nameText.text = "Enemy";
        }

        gameObject.name = "Player " + _playerId;
    }

    // Set the local player view to bottom of the screen
    public override void OnStartLocalPlayer()
    {
        transform.position = Constants.LOCAL_PLAYER_POSITION;
        _nameText.text = "You";
    }

    public void UpdateHealth(int newHealth)
    {
        if (!isServer)
            return;

        _health = newHealth;
    }

    public void UpdateEnergy(int newEnergy)
    {
        if (!isServer)
            return;
        
        _energy = newEnergy;
    }

    private void OnChangeHealth(int currentHealth)
    {
        // Update visual with current health
        _healthText.text = "Health : " + currentHealth;

    }

    private void OnChangeEnergy(int currentEnergy)
    {
        // Update visual with current health
        _energyText.text = "Energy : " + currentEnergy;
    }

    private void OnChangePlayerID(int playerId)
    {
        _playerId = playerId;
    }

    /*** TODO : SHOULD BE MADE INTO ITS OWN CLASS THAT HANDLES INPUT FROM PLAYER VIEW PREFAB **/

    //*** CURRENTLY USED TO TEST SYNCING OF CLIENTS IS WORKING **//
    //** SHOULD BE USED TO INSTANTIATE THE RELEASE OF ENERGIES **//
    public void OnMouseDown()
    {
        if (!isLocalPlayer)
            return;

        CmdPlayerShoot();
    }
    //*** CURRENTLY USED TO TEST SYNCING OF CLIENTS IS WORKING **//
    //** SHOULD BE USED TO INSTANTIATE THE RELEASE OF ENERGIES **//
    [Command]
    public void CmdPlayerShoot()
    {
        _energySpawner.SpawnEnergy(0, transform.position, _playerId);
    }
}
