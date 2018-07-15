using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager {

    private PlayerModel[] _players;

    public PlayerManager()
    {
        _players = new PlayerModel[2];

        _players[Constants.PLAYER_ONE] = new PlayerModel(3, 3);
        _players[Constants.PLAYER_TWO] = new PlayerModel(3, 3);
    }

    public int GetPlayersCurrentEnergy(int id)
    {
        return _players[id].GetEnergy();
    }

    public int GetPlayersCurrentHealth(int id)
    {
        return _players[id].GetHealth();
    }

    public void AddEnergyToPlayers()
    {
        foreach(PlayerModel player in _players)
        {
            player.GainEnergy(1);
        }
    }

    public bool PlayerCanCastEnergyOfCost(int id, int energyCost)
    {
        return _players[id].HasEnoughEnergy(energyCost);
    }

    public void PlayerCastsEnergy(int id, int energyCost)
    {
        _players[id].SpendEnergy(energyCost); 
    }
	
    public void DamagePlayer(int id, int damage)
    {
        _players[id].Damage(damage);
    }
}
