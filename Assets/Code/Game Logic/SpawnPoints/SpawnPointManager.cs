using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager {

    private List<SpawnPoint> _spawnPoints;

    public void SetSpawnPoints(List<SpawnPoint> spawnPoints)
    {
        _spawnPoints = spawnPoints;
    }

    public Vector3 GetSpawnPointPosition(int spawnPointId)
    {
        return _spawnPoints[spawnPointId].GetPosition();
    }
}
