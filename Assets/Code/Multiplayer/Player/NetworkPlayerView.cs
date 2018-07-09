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

    // Set the non-local players view to the top of the screen
    public override void OnStartClient()
    {
        if (!isLocalPlayer)
        {
            transform.position = Constants.NON_LOCAL_PLAYER_POSITION;
            _nameText.text = "Enemy";
        }
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

}
