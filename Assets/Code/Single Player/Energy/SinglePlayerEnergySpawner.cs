﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerEnergySpawner : BaseEnergySpawner
{
    public GameObject energyPrefab;

    public override void SpawnEnergy(int energyIndex, Vector3 position, int playerId)
    {
        EnergyType energyType = _energyTypes[energyIndex];

        GameObject energy = GameObject.Instantiate(energyPrefab, position, Quaternion.identity);

        EnergyView energyView = energy.GetComponent<EnergyView>();

        energyView.SetOwningPlayer(playerId);
        energyView.SetSpeed(energyType.GetSpeed());
    }
}
