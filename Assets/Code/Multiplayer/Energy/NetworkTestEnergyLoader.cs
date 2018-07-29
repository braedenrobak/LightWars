using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTestEnergyLoader : IEnergyLoader
{

    private int[] _cost = { 1, 2, 3 };
    private int[] _damage = { 1, 2, 3 };
    private int[] _health = { 1, 2, 3 };
    private float[] _speed = { 2.5f, 3.5f, 4.5f };
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

    public float GetSpeed()
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
