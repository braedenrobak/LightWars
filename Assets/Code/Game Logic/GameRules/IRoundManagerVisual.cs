using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoundManagerVisual {

    bool RoundVisualHasFinished();

    void EndRound(int winner);

    void StartGame();

    void EndGame(int winner);
}
