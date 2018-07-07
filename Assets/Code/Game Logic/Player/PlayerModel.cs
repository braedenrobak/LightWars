using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : IKillable<int>
{
    private int _health;
    private int _energy;

    public PlayerModel(int health, int energy)
    {
        _health = health;
        _energy = energy;
    }

    public int GetHealth()
    {
        return _health;
    }

    public int GetEnergy()
    {
        return _energy;
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

    public void Destroy()
    {
        // Play destroy animation
    }

    public void GainEnergy(int energyAmount)
    {
        _energy += energyAmount;
    }


    public bool HasEnoughEnergy(int energyCost)
    {
        return _energy >= energyCost;
    }

    public void SpendEnergy(int energyCost)
    {
        _energy -= energyCost;
    }

}
