using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager {

    private int _numberOfRounds;
    private int _currentRound;

    private int _winner;
    private bool _hasWinner;

    private List<int> _roundWinners;

    public RoundManager(int numberOfRounds)
    {
        _numberOfRounds = numberOfRounds;
        _currentRound = 1;

        _winner = -1;
        _hasWinner = false;

        _roundWinners = new List<int>();
    }

    public int NumberOfRounds()
    {
        return _numberOfRounds;
    }

    public int GetBestOfRoundCount()
    {
        return (_numberOfRounds / 2) + 1;
    }

    public int GetCurrentRound()
    {
        return _currentRound;
    }

    public void EndRound(int winnerOfRoundID)
    {
        _roundWinners.Add(winnerOfRoundID);
        _currentRound++;

        CalculateWinner();
    }

    public bool HasWinner()
    {
        return _hasWinner;
    }

    public int GetWinner()
    {
        return _winner;
    }

    public List<int> GetRoundWinners()
    {
        return _roundWinners;
    }

    private void CalculateWinner()
    {
        List<int> winners = _roundWinners;

        winners.Sort();

        int roundCount = 0;
        int lastWinner = -1;
        foreach (int winnerId in winners)
        {
            if (winnerId == lastWinner)
            {
                roundCount++;
            }
            else
            {
                roundCount = 1;
            }

            lastWinner = winnerId;

            if (roundCount == GetBestOfRoundCount())
            {
                _winner = winnerId;
                _hasWinner = true;
                break;
            }
        }
    }
}
