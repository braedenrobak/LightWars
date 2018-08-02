using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullRoundManagerVisual : IRoundManagerVisual
{
    public void EndRound(int winner)
    {
        // Stubbed
    }

    public bool RoundVisualHasFinished()
    {
        return true;
    }
}
