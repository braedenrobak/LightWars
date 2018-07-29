using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnergyLoader : IEnergyLoader
{
    private int _index = 0;

    public int GetColour()
    {
        return 0;
    }

    public int GetCost()
    {
        return 1;
    }

    public int GetDamage()
    {
        return 1;
    }

    public string GetEnergyTypeName()
    {
        return "Name";
    }

    public int GetHealth()
    {
        return 1;
    }

    public float GetSpeed()
    {
        return 1.0f;
    }

    public bool HasNextEnergy()
    {
        if (_index != 1)
            return true;

        return false;
    }

    public void LoadNextEnergy()
    {
        _index++;
    }
}
