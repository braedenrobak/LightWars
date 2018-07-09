using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerIds
{
    PlayerOne = 0,
    PlayerTwo = 1
};


public class PlayerController {

    private PlayerModel[] _players;

    public PlayerController()
    {
        _players = new PlayerModel[2];

        _players[(int)PlayerIds.PlayerOne] = new PlayerModel(3, 3);
        _players[(int)PlayerIds.PlayerTwo] = new PlayerModel(3, 3);
    }

    public int GetPlayersCurrentEnergy(PlayerIds id)
    {
        return _players[(int)id].GetEnergy();
    }

    public int GetPlayersCurrentHealth(PlayerIds id)
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

    public bool PlayerCanCastEnergyOfCost(PlayerIds id, int energyCost)
    {
        return _players[(int)id].HasEnoughEnergy(energyCost);
    }

    public void PlayerCastsEnergy(int id, int energyCost)
    {
        _players[id].SpendEnergy(energyCost); 
    }
	
    public void DamagePlayer(PlayerIds id, int damage)
    {
        _players[(int)id].Damage(damage);
    }
}
