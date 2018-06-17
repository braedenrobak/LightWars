using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable<T> {
    void Damage(T dmg);

    void Heal(T heal);

    bool IsDead();
}
