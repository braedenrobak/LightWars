using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerEntityTests {

    private PlayerEntity playerEntity;

    [SetUp]
    public void Setup()
    {
        playerEntity = new PlayerEntity(3, 3);
    }

    [TearDown]
    public void TearDown()
    {
        playerEntity = null;
    }

    [Test]
    public void TakesDamage()
    {
        playerEntity.Damage(1);

        Assert.AreEqual(2, playerEntity.GetHealth());
    }

    [Test]
    public void TrueOnDeathWhenHealthIsEqualOrBelowZero()
    {
        playerEntity.Damage(3);

        Assert.True(playerEntity.IsDead());
    }

    [Test]
    public void FalseOnNotEnoughEnergy()
    {
        Assert.False(playerEntity.HasEnoughEnergy(4));
    }

    [Test]
    public void TrueOnEnoughEnergy()
    {
        Assert.True(playerEntity.HasEnoughEnergy(3));
    }

    [Test]
    public void ProperAmountOfEnergyTakenAway()
    {
        playerEntity.SpendEnergy(2);

        Assert.AreEqual(1, playerEntity.GetEnergy());
    }

}
