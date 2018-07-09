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
        energy.GetComponent<NetworkEnergyView>().SetOwner(playerId);
        energy.GetComponent<NetworkEnergyView>().SetSpeed(1);
        NetworkServer.Spawn(energy);
    }
}
