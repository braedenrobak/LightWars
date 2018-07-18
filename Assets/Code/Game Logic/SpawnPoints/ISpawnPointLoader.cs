using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnPointLoader {

    bool HasAnotherSpawnPoint();

    void LoadNextSpawnPoint();

    int GetOwner();

    int GetSibilingId();

    Vector3 GetPosition();
}
