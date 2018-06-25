using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerViewOutputController {

    void DisplayPlayerHit(PlayerIds playerId, int damage);

    void UpdatePlayerView(PlayerData playerData);

    void GameOverWithWinner(PlayerIds playerId);
}
