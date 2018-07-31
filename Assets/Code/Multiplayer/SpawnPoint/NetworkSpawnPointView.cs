using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSpawnPointView : NetworkBehaviour {

    private Vector3 _endPosition;

    private NetworkPlayerInput _networkPlayerInput;

    private bool _spawnClicked = false;
    private bool _clickTracking = false;

    private int _clickCount = 0;

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
        Input.simulateMouseWithTouches = true;

        if(_ownerId == Constants.LOCAL_PLAYER_ID)
        {
            transform.position = new Vector3(transform.position.x, Constants.LOCAL_PLAYER_POSITION.y, -1.0f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Constants.NON_LOCAL_PLAYER_POSITION.y, -1.0f);
        }

        gameObject.name = "Owned by " + _ownerId;
    }

	public void Start()
	{
        Vector3 ownerPosition = GameObject.Find("Player " + _ownerId).transform.position;
	}

	public void SetPlayerInput(NetworkPlayerInput networkPlayerInput)
    {
        _networkPlayerInput = networkPlayerInput;
    }

	public void OnMouseDown()
    {
        _spawnClicked = true;

        if(!_clickTracking)
        {
            _clickTracking = true;
            StartCoroutine(StartClickTracking());
        }
    }

    private IEnumerator StartClickTracking()
    {
        float clickWindow = 0.2f;

        while(clickWindow > 0.0f && _clickCount < 3)
        {
            clickWindow -= Time.deltaTime;

            if(_spawnClicked)
            {
                _spawnClicked = false;
                _clickCount++;
                clickWindow = 0.2f;
            }
            yield return null;
        }

        GameObject.Find("Player " + _ownerId).GetComponent<NetworkPlayerInput>().ShootEnergy(_clickCount, gameObject.transform.position);
        _clickCount = 0;
        _clickTracking = false;
    }
}
