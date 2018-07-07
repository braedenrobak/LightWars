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

    private int _playerId;

    private GameManager _gameManager;

    // All about updating visual
    // This is where you connect the variables to be chagned across clients when changed

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
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
            transform.position = new Vector3(0.0f, 3.0f, 0.0f);
            _nameText.text = "Enemy";
        }
    }

    // Set the local player view to bottom of the screen
    public override void OnStartLocalPlayer()
    {
        transform.position = new Vector3(0.0f, -3.0f, 0.0f);
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

    /*** TODO : SHOULD BE MADE INTO ITS OWN CLASS THAT HANDLES INPUT FROM PLAYER VIEW PREFAB **/

    //*** CURRENTLY USED TO TEST SYNCING OF CLIENTS IS WORKING **//
    //** SHOULD BE USED TO INSTANTIATE THE RELEASE OF ENERGIES **//
    public void OnMouseDown()
    {
        if (!isLocalPlayer)
            return;

        CmdPlayerHit();
    }
    //*** CURRENTLY USED TO TEST SYNCING OF CLIENTS IS WORKING **//
    //** SHOULD BE USED TO INSTANTIATE THE RELEASE OF ENERGIES **//
    [Command]
    public void CmdPlayerHit()
    {
        EnergyData energyData = new EnergyData();
        energyData.damage = 1;
        energyData.energyType = 1;
        _gameManager.PlayerHit(new PlayerData(_playerId, _health, _energy),  energyData);
    }
}
