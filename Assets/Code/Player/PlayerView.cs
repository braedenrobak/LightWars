using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour {

    public Text _healthText;
    public Text _energyText;

    private GameController _gameController;

    private PlayerData _playerData;


    public PlayerView(GameController gameController, PlayerData playerData)
    {
        _gameController = gameController;
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

        _gameController.PlayerHit(_playerData, energyData);
    }

    private void DisplayPlayer()
    {
        // Use player data to update the different UI elements for health and energy
        _healthText.text = "Health : " + _playerData.health;
        _energyText.text = "Energy : " + _playerData.energy;
    }
}
