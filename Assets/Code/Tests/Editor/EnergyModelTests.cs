using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnergyModelTests {

    private EnergyModel energyModel;


    [SetUp]
    public void Setup()
    {
        energyModel = new EnergyModel(3, 2, 5);
    }

    [Test]
    public void ProperInitializationOfHealthDamageAndSpeed()
    {
        Assert.AreEqual(3, energyModel.GetHealth());
        Assert.AreEqual(2, energyModel.GetDamage());
        Assert.AreEqual(5, energyModel.GetSpeed());
    }

    [Test]
    public void TakesDamageProperly()
    {
        energyModel.Damage(2);

        Assert.AreEqual(1, energyModel.GetHealth());
    }

    [Test]
    public void TrueOnIsDeadEqualToZero()
    {
        energyModel.Damage(3);

        Assert.True(energyModel.IsDead());
    }

    [Test]
    public void TrueOnIsDeadLessThanZero()
    {
        energyModel.Damage(4);

        Assert.True(energyModel.IsDead());
    }
}
