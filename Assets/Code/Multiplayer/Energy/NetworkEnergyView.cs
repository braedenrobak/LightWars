using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkEnergyView : NetworkBehaviour {

    [SyncVar(hook = "UpdateOwnerId")]
    private int _ownerId = -1;
    private int _speed = -1;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private float directionInverter = 1.0f;

    [SyncVar(hook = "UpdatePercent")]
    private float _percentFinished = 0.0f;

    public void SetOwner(int playerId)
    {
        _ownerId = playerId;
    }

    private void UpdateOwnerId(int ownerId)
    {
        _ownerId = ownerId;
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
            if (isServer)
                directionInverter = 1.0f;
        }
        else
        {
            _endPosition = Constants.LOCAL_PLAYER_POSITION;
            if (isServer)
                directionInverter = -1.0f;
        }
    }


    public void Update()
    {
        if (!isServer)
        {
            return;
        }

        Vector3 currentDesiredPosition = transform.position + new Vector3(0, (_speed * Time.deltaTime * directionInverter));

        _percentFinished = InverseLerp(_startPosition, _endPosition, currentDesiredPosition);
        RpcUpdatePosition();

        transform.position = Vector3.Lerp(_startPosition, _endPosition, _percentFinished);
    }

    [ClientRpc]
    public void RpcUpdatePosition()
    {
        transform.position = Vector3.Lerp(_startPosition, _endPosition, _percentFinished);
    }

    private void UpdatePercent(float percentToFinish)
    {
        _percentFinished = percentToFinish;
    }

    public static float InverseLerp(Vector3 start, Vector3 end, Vector3 current)
    {
        Vector3 AB = end - start;
        Vector3 AV = current - start;
        return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
    }
}
