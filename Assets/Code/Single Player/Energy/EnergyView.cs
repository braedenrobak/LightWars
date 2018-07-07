using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyView : MonoBehaviour {

    private int _speed;
    private int _ownerId;

    public void Awake()
    {
        _speed = 0;
        _ownerId = -1;
    }

    public void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (_speed * Time.deltaTime));
    }


    public void SetOwningPlayer(int playerId)
    {
        _ownerId = playerId;
    }

    public void SetSpeed(int speed)
    {
        _speed = speed;
    }

    public int GetOwningPlayer()
    {
        return _ownerId;
    }
}
