using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkEnergyView : NetworkBehaviour, IEnergySpawnable {

    [SyncVar(hook = "UpdateOwnerId")]
    private int _ownerId = -1;
    private void UpdateOwnerId(int ownerId)
    {
        _ownerId = ownerId;
    }

    [SyncVar(hook = "UpdateSpeed")]
    private int _speed = -1;
    public void UpdateSpeed(int speed)
    {
        _speed = speed;
    }

    private float directionInverter = 1.0f;

    public void InitializeEnergy(int ownerId, int speed)
    {
        _ownerId = ownerId;
        _speed = speed;
    }

    public void Start()
    {
        Vector3 startPosition = GameObject.Find("Player " + _ownerId).transform.position;
        transform.position = startPosition;

        if (startPosition == Constants.LOCAL_PLAYER_POSITION)
        {
            directionInverter = 1.0f;
        }
        else
        {
            directionInverter = -1.0f;
        }
    }


    public void Update()
    {
        transform.position += new Vector3(0, (_speed * Time.deltaTime * directionInverter));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NetworkPlayerInput>().GetPlayerId() != _ownerId)
        {
            collision.GetComponent<NetworkPlayerInput>().PlayerHit(0);

            Destroy(gameObject);
        }
    }
}
