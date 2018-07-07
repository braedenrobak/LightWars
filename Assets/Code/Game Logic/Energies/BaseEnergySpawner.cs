using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnergySpawner {
    
    private IEnergyLoader _energyLoader;
    protected List<EnergyType> _energyTypes;

    public abstract void SpawnEnergy(int energyIndex, Vector3 position, int playerId);

    protected BaseEnergySpawner()
    {
        _energyTypes = new List<EnergyType>();
    }

    public void SetEnergyLoader(IEnergyLoader energyLoader)
    {
        _energyLoader = energyLoader;
    }

    public List<EnergyType> GetEnergyTypes()
    {
        return _energyTypes;
    }

    public void LoadEnergies()
    {
        while(_energyLoader.HasNextEnergy())
        {
            _energyLoader.LoadNextEnergy();

            _energyTypes.Add(new EnergyType(_energyLoader.GetHealth(), _energyLoader.GetCost(), _energyLoader.GetSpeed(), _energyLoader.GetEnergyTypeName()));
        }
    }
}
