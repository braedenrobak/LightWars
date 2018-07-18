using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSpawnPointSpawner : BaseSpawnPointSpawner {

    public GameObject spawnPointPrefab;

    private List<NetworkPlayerInput> _networkPlayerInputs;

    public NetworkSpawnPointSpawner()
    {
        _networkPlayerInputs = new List<NetworkPlayerInput>();
    }

    public void SetNetworkPlayerInputs(List<NetworkPlayerInput> networkPlayerInputs)
    {
        _networkPlayerInputs = networkPlayerInputs;
    }

    public override void LoadSpawnPoints()
    {
        base.LoadSpawnPoints();

        foreach(SpawnPoint spawnPoint in _spawnPoints)
        {
            GameObject spawnPointGO = GameObject.Instantiate(spawnPointPrefab, spawnPoint.GetPosition(), Quaternion.identity);
            spawnPointGO.GetComponent<NetworkSpawnPointView>().SetPlayerInput(_networkPlayerInputs[spawnPoint.GetOwnerId()]);
            spawnPointGO.GetComponent<NetworkSpawnPointView>().SetOwnerId(spawnPoint.GetOwnerId());
            Vector3 endPosition = _spawnPointManager.GetSpawnPointPosition(spawnPoint.GetSibling());
            NetworkServer.Spawn(spawnPointGO);
        }
    }
}
