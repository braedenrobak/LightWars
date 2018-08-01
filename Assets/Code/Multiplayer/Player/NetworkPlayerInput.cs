using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerInput : NetworkBehaviour {

    private GameManager _gameManager;
    private BaseEnergySpawner _energySpawner;

    [SyncVar(hook = "UpdatePlayerId")]
    private int _playerId;
    private void UpdatePlayerId(int playerId)
    {
        _playerId = playerId;
    }

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

    public int GetPlayerId()
    {
        return _playerId;
    }

    public override void OnStartClient()
    {
        gameObject.name = "Player " + _playerId;
    }

	public override void OnStartLocalPlayer()
	{
        Constants.LOCAL_PLAYER_ID = _playerId;
	}

	public void PlayerHit(int energyType)
    {
        if (!isServer)
            return;

        EnergyData energyData = new EnergyData();

        energyData.damage = 1;

        energyData.energyType = energyType;

        _gameManager.PlayerHit(new PlayerData(_playerId, 0, 0), energyData);
    }

    public void ShootEnergy(int cost, Vector3 position)
    {
        if (!isLocalPlayer)
            return;
        CmdPlayerShoot(cost, position);
    }

    [Command]
    public void CmdPlayerShoot(int energyCost, Vector3 position)
    {
        if (_gameManager.PlayerCastEnergy(_playerId, energyCost))
        {
            _energySpawner.SpawnEnergy(energyCost, position, _playerId);
        }
        else
        {
            Debug.Log("Player does not have enough energy!");
        }
    }
}
