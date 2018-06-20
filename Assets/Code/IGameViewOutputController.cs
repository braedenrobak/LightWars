using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameViewOutputController {

    void DisplayPlayerHit(PlayerIds playerId, int damage);

    void UpdatePlayerView(PlayerData playerData);

    void GameOverWithWinner(PlayerIds playerId);
}
