using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameViewOutputController {

    void DisplayPlayerHit(Players playerId, int damage);

    void UpdatePlayerView(PlayerData playerData);

    void GameOverWithWinner(Players playerId);
}
