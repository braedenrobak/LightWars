using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTestEnergyLoader : IEnergyLoader
{

    private int[] _cost = { 1, 2, 3 };
    private int[] _damage = { 1, 2, 3 };
    private int[] _health = { 1, 2, 3 };
    private int[] _speed = { 3, 2, 1 };
    private string[] _name = { "FastWeak", "Average", "SlowStrong" };

    private int _index = -1;

    public int GetColour()
    {
        throw new System.NotImplementedException();
    }

    public int GetCost()
    {
        return _cost[_index];
    }

    public int GetDamage()
    {
        return _damage[_index];
    }

    public string GetEnergyTypeName()
    {
        return _name[_index];
    }

    public int GetHealth()
    {
        return _health[_index];
    }

    public int GetSpeed()
    {
        return _speed[_index];
    }

    public bool HasNextEnergy()
    {
        return _index < _cost.Length - 1;
    }

    public void LoadNextEnergy()
    {
        _index++;
    }
}
