using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerViewOutputController {

    void DisplayPlayerHit(int playerId, int damage);

    void UpdatePlayerView(PlayerData playerData);

    void GameOverWithWinner(int playerId);
}
