using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyModel : IKillable<int> {
    
	private int _health;
    private int _damage;
    private int _speed;

    public EnergyModel(int health, int damage, int speed)
	{
        this._health = health;
		this._damage = damage;
		this._speed = speed;
	}

    public int GetHealth()
    {
        return _health;
    }

    public int GetDamage()
    {
        return _damage;
    }

    public int GetSpeed()
    {
        return _speed;
    }

    public void Damage(int dmg)
    {
        _health -= dmg;
    }

    public void Heal(int heal)
    {
        _health += heal;
    }

    public bool IsDead()
    {
        return _health <= 0;
    }
}
