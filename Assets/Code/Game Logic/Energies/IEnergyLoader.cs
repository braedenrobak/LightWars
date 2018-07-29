using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnergyLoader {

    bool HasNextEnergy();

    void LoadNextEnergy();

    int GetHealth();

    float GetSpeed();

    int GetDamage();

    int GetCost();

    int GetColour();

    string GetEnergyTypeName();
}
