using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Players
{
    PlayerOne = 0,
    PlayerTwo = 1
};


public class GameManager {

    private PlayerModel[] _players;

    public GameManager()
    {
        _players = new PlayerModel[2];

        _players[(int)Players.PlayerOne] = new PlayerModel(3, 3);
        _players[(int)Players.PlayerTwo] = new PlayerModel(3, 3);
    }

    public int GetPlayersCurrentEnergy(Players id)
    {
        return _players[(int)id].GetEnergy();
    }

    public int GetPlayersCurrentHealth(Players id)
    {
        return _players[(int)id].GetHealth();
    }

    public void AddEnergyToPlayers()
    {
        foreach(PlayerModel player in _players)
        {
            player.GainEnergy(1);
        }
    }

    public bool PlayerCanCastEnergyOfCost(Players id, int energyCost)
    {
        return _players[(int)id].HasEnoughEnergy(energyCost);
    }
	
    public void DamagePlayer(Players id, int damage)
    {
        _players[(int)id].Damage(damage);
    }
}
