using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct EnergyObjectData
{
    public int Cost;
    public int Health;
    public int Damage;
    public int Speed;
    public int Colour;
}

public struct EnergyType
{
    public int energyIndex;
    public int energyColour;
    public int energyCost;
    public string energyName;
}

public class EnergyManager  {
    
    private IEnergyLoader _energyLoader;
    private List<EnergyObjectData> _energyObjects;
    private List<EnergyType> _energyTypes;

    public EnergyManager()
    {
        _energyObjects = new List<EnergyObjectData>();
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
        int index = 0;
        while(_energyLoader.HasNextEnergy())
        {
            _energyLoader.LoadNextEnergy();

            EnergyObjectData newEnergyObject = new EnergyObjectData();
            newEnergyObject.Cost = _energyLoader.GetCost();
            newEnergyObject.Damage = _energyLoader.GetDamage();
            newEnergyObject.Health = _energyLoader.GetHealth();
            newEnergyObject.Speed = _energyLoader.GetSpeed();
            newEnergyObject.Colour = _energyLoader.GetColour();

            _energyObjects.Add(newEnergyObject);

            EnergyType energyType = new EnergyType();
            energyType.energyName = _energyLoader.GetEnergyTypeName();
            energyType.energyColour = newEnergyObject.Colour;
            energyType.energyCost = newEnergyObject.Cost;
            energyType.energyIndex = index;

            _energyTypes.Add(energyType);
            index++;
        }
    }

    public EnergyObjectData GetEnergyOfType(int energyTypeIndex)
    {
        return _energyObjects[energyTypeIndex];
    }
}
