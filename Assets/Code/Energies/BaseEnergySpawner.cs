using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnergySpawner {

    // Give something for player input to know it is able to call to spawn an energy with certain type and position
    protected EnergyManager _energyManager;

    public abstract void SpawnEnergyOfType(int energyType, Vector3 position);

    public void SetEnergyManager(EnergyManager energyManager)
    {
        _energyManager = energyManager;
    }

    public List<EnergyType> GetEnergiesThatCanSpawn()
    {
        return _energyManager.GetEnergyTypes();
    }

    private EnergyObjectData GetEnergyObjectDataOfType(int energyType)
    {
        return _energyManager.GetEnergyOfType(energyType);
    }
}
