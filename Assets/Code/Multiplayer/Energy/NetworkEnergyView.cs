using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkEnergyView : NetworkBehaviour {

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

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private float directionInverter = 1.0f;

    private float _percentFinished = 0.0f;

    public void SetOwner(int playerId)
    {
        _ownerId = playerId;
    }

    public void SetSpeed(int speed)
    {
        _speed = speed;
    }

    public void Start()
    {
        _startPosition = GameObject.Find("Player " + _ownerId).transform.position;
        transform.position = _startPosition;

        if (_startPosition == Constants.LOCAL_PLAYER_POSITION)
        {
            _endPosition = Constants.NON_LOCAL_PLAYER_POSITION;
            directionInverter = 1.0f;
        }
        else
        {
            _endPosition = Constants.LOCAL_PLAYER_POSITION;
            directionInverter = -1.0f;
        }
    }


    public void Update()
    {
        Vector3 currentDesiredPosition = transform.position + new Vector3(0, (_speed * Time.deltaTime * directionInverter));

        _percentFinished = PercentageInbetween(_startPosition, _endPosition, currentDesiredPosition);

        transform.position = Vector3.Lerp(_startPosition, _endPosition, _percentFinished);
    }

    private float PercentageInbetween(Vector3 start, Vector3 end, Vector3 current)
    {
        float totalDistance = Vector3.Distance(start, end);
        float currentDistance = Vector3.Distance(current, end);

        return 1.0f - (currentDistance / totalDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NetworkPlayerView>().GetId() != _ownerId)
        {
            collision.GetComponent<NetworkPlayerView>().PlayerHit(0);

            Destroy(gameObject);
        }
    }
}
