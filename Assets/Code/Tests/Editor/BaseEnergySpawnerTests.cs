using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;


public class MockEnergyLoader : IEnergyLoader
{
    private bool _hasNextEnergy = true;

    private int _numberOfEnergies = 2;

    private int _offset = -1;

    public int GetColour()
    {
        return 255 + _offset;
    }

    public int GetCost()
    {
        return 1 + _offset;
    }

    public int GetDamage()
    {
        return 1 + _offset;
    }

    public string GetEnergyTypeName()
    {
        return "TestName" + _offset;
    }

    public int GetHealth()
    {
        return 3 + _offset;
    }

    public float GetSpeed()
    {
        return 2.0f + _offset;
    }

    public bool HasNextEnergy()
    {
        return _hasNextEnergy;
    }

    public void LoadNextEnergy()
    {
        _numberOfEnergies--;
        _offset++;
        if (_numberOfEnergies <= 0)
            _hasNextEnergy = false;
    }
}

public class MockEnergySpawner : BaseEnergySpawner
{
    public override void SpawnEnergy(int energyIndex, Vector3 position, int playerId)
    {
        Debug.Log("Mock Energy Spawner spawns energy type " + energyIndex + " for player " + playerId + " at position " + position);
    }
}

public class BaseEnergySpawnerTest {

    private BaseEnergySpawner _energyManager;

    [SetUp]
    public void Setup()
    {
        _energyManager = new MockEnergySpawner();
        _energyManager.SetEnergyLoader(new MockEnergyLoader());
    }

    [TearDown]
    public void Teardown()
    {
        _energyManager = null;
    }

    [Test]
    public void EnergiesAndTheirRespectiveTypesConnectedProperly()
    {
        _energyManager.LoadEnergies();

        List<EnergyType> _energyTypes = _energyManager.GetEnergyTypes();

        Assert.AreEqual(2, _energyTypes.Count);

        Assert.AreEqual(3, _energyTypes[0].GetHealth());
        Assert.AreEqual(1, _energyTypes[0].GetCost());
        Assert.AreEqual(2, _energyTypes[0].GetSpeed());
        Assert.AreEqual("TestName0", _energyTypes[0].GetName());

        Assert.AreEqual(4, _energyTypes[1].GetHealth());
        Assert.AreEqual(2, _energyTypes[1].GetCost());
        Assert.AreEqual(3, _energyTypes[1].GetSpeed());
        Assert.AreEqual("TestName1", _energyTypes[1].GetName());

    }


}
