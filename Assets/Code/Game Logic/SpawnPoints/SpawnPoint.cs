using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint {

    private int _siblingSpawnPoint;

    private int _ownerId;

    private Vector3 _position;

    public void SetPosition(Vector3 position)
    {
        _position = position;
    }

    public void SetSiblingSpawnPoint(int sibling)
    {
        _siblingSpawnPoint = sibling;
    }

    public void SetOwningPlayer(int playerId)
    {
        _ownerId = playerId;
    }

    public Vector3 GetPosition()
    {
        return _position;
    }

    public int GetOwnerId()
    {
        return _ownerId;
    }

    public int GetSibling()
    {
        return _siblingSpawnPoint;
    }
}
