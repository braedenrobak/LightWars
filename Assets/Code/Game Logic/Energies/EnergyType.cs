using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyType {
    
	private int _health;
    private int _cost;
    private float _speed;
    private string _name;


    public EnergyType(int health, int cost, float speed, string name)
	{
        _health = health;
		_cost = cost;
		_speed = speed;
        _name = name;
	}

    public int GetHealth()
    {
        return _health;
    }

    public int GetCost()
    {
        return _cost;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public string GetName()
    {
        return _name; 
    }

}
