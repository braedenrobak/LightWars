using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkEnergySpawner : BaseEnergySpawner
{
    public GameObject energyPrefab;

    public override void SpawnEnergy(int energyIndex, Vector3 position, int playerId)
    {
        GameObject energy = GameObject.Instantiate(energyPrefab, position, Quaternion.identity);
        energy.GetComponent<IEnergySpawnable>().InitializeEnergy(playerId, _energyTypes[energyIndex-1].GetSpeed());
        NetworkServer.Spawn(energy);
    }
}
