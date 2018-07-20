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

    // Set the non-local players view to the top of the screen
    public override void OnStartClient()
    {
        Vector3 ownerPosition = GameObject.Find("Player " + _ownerId).transform.position;
        if (!isLocalPlayer)
        {
            transform.position = new Vector3(transform.position.x, ownerPosition.y, -1.0f);
        }

        gameObject.name = "Owned by " + _ownerId;
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
