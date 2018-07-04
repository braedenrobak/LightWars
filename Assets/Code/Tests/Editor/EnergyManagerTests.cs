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

    public int GetSpeed()
    {
        return 2 + _offset;
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


public class EnergyManagerTests {

    private EnergyManager _energyManager;

    [SetUp]
    public void Setup()
    {
        _energyManager = new EnergyManager();
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

        List<EnergyType> energyTypes = _energyManager.GetEnergyTypes();

        // Test a random one (??)
        Assert.AreEqual(2, energyTypes.Count);

        int selectedEnergyToTestType = energyTypes[1].energyIndex;

        EnergyObjectData selectedEnergyData = _energyManager.GetEnergyOfType(selectedEnergyToTestType);

        Assert.AreEqual(2, selectedEnergyData.Cost);
        Assert.AreEqual(2, selectedEnergyData.Damage);
        Assert.AreEqual(4, selectedEnergyData.Health);
        Assert.AreEqual(3, selectedEnergyData.Speed);
        Assert.AreEqual(256, selectedEnergyData.Colour);

        Assert.AreEqual(energyTypes[1].energyCost, selectedEnergyData.Cost);
        Assert.AreEqual(energyTypes[1].energyColour, selectedEnergyData.Colour);
    }

    [Test]
    public void CreatesAndFormatsTypes()
    {
        _energyManager.LoadEnergies();
        List<EnergyType> energyTypes = _energyManager.GetEnergyTypes();

        Assert.AreEqual(2, energyTypes.Count);

        Assert.AreEqual(1, energyTypes[0].energyCost); ;
        Assert.AreEqual(255, energyTypes[0].energyColour);
        Assert.AreEqual(0, energyTypes[0].energyIndex);
        Assert.AreEqual("TestName0", energyTypes[0].energyName);

        Assert.AreEqual(2, energyTypes[1].energyCost);;
        Assert.AreEqual(256, energyTypes[1].energyColour);
        Assert.AreEqual(1, energyTypes[1].energyIndex);
        Assert.AreEqual("TestName1", energyTypes[1].energyName);
    }


}
