using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSpawnPointSpawner : BaseSpawnPointSpawner {

    public GameObject spawnPointPrefab;

    public override void LoadSpawnPoints()
    {
        base.LoadSpawnPoints();

        foreach(SpawnPoint spawnPoint in _spawnPoints)
        {
            GameObject spawnPointGO = GameObject.Instantiate(spawnPointPrefab, spawnPoint.GetPosition(), Quaternion.identity);
            spawnPointGO.GetComponent<NetworkSpawnPointView>().SetOwnerId(spawnPoint.GetOwnerId());
            NetworkServer.Spawn(spawnPointGO);
        }
    }
}
