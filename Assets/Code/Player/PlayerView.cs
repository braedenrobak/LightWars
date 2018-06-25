using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour {

    public Text _healthText;
    public Text _energyText;

    private GameManager _gameManager;

    private PlayerData _playerData;


    public PlayerView(GameManager gameController, PlayerData playerData)
    {
        _gameManager = gameController;
        _playerData = playerData;
    }

    public void DisplayHit(int damage)
    {
        // Play hit animation
        Debug.Log("HIT ANIMATION DISPLAYED NOW! TODO : CREATE PLAYER HIT ANIMATION");
        // Display damage to screen (ie damage popping up and fading away)
        Debug.Log("PLAYER HIT WITH " + damage + " DAMAGE! TODO CREATE DAMAGE NUMBER DISPLAY");
    }

    public void UpdateVisual(PlayerData playerData)
    {
        _playerData = playerData;

        DisplayPlayer();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        EnergyData energyData = new EnergyData();//collision.gameObject.GetComponent<EnergyView>().GetEnergyData();

        _gameManager.PlayerHit(_playerData, energyData);
    }

    private void DisplayPlayer()
    {
        // Use player data to update the different UI elements for health and energy
        _healthText.text = "Health : " + _playerData.health;
        _energyText.text = "Energy : " + _playerData.energy;
    }
}
