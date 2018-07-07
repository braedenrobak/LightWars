using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour {

    public Text _healthText;
    public Text _energyText;

    private GameManager _gameManager;

    private PlayerData _playerData;

    private BaseEnergySpawner _energySpawner;

    public void InitializePlayerView(GameManager gameController, BaseEnergySpawner energySpawner)
    {
        _gameManager = gameController;
        _energySpawner = energySpawner;
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
        if (collision.GetComponent<EnergyView>().GetOwningPlayer() != (int)_playerData.id)
        {
            EnergyData energyData = new EnergyData();

            energyData.damage = 1;

            _gameManager.PlayerHit(_playerData, energyData);

            Destroy(collision.gameObject);
        }
    }

    private void DisplayPlayer()
    {
        // Use player data to update the different UI elements for health and energy
        _healthText.text = "Health : " + _playerData.health;
        _energyText.text = "Energy : " + _playerData.energy;
    }

    private void OnMouseDown()
    {
        _energySpawner.SpawnEnergy(0, Camera.main.ScreenToWorldPoint(Input.mousePosition), (int)_playerData.id);
    }
}
