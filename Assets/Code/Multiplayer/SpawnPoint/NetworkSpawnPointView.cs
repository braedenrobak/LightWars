using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSpawnPointView : NetworkBehaviour {

    private Vector3 _endPosition;

    private NetworkPlayerInput _networkPlayerInput;

    [SyncVar (hook = "ChangedOwnerId")]
    private int _ownerId;
    public void ChangedOwnerId(int ownerId)
    {
        _ownerId = ownerId;
    }

    public void SetOwnerId(int ownerId)
    {
        _ownerId = ownerId;
    }
    public void SetPlayerInput(NetworkPlayerInput networkPlayerInput)
    {
        _networkPlayerInput = networkPlayerInput;
    }

    public void OnMouseDown()
    {
        // Tell energySpawner to spawn energy at position
        GameObject.Find("Player " + _ownerId).GetComponent<NetworkPlayerInput>().ShootEnergy(1, gameObject.transform.position);
    }
}
