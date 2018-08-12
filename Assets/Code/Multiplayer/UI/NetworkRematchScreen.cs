﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkRematchScreen : NetworkBehaviour {

    public Text _outcomeText;

    public NetworkMain networkMain;

    private GameObject _rematchCanvas;

    [SyncVar (hook = "UpdateRematchCounter")]
    private int _rematchCounter = 0;

    private void UpdateRematchCounter(int rematchCounter)
    {
        Debug.LogError("Old ==> " + _rematchCounter + " New ==> " + rematchCounter);
        _rematchCounter = rematchCounter;
        if(isServer && _rematchCounter == 2)
        {
            networkMain.Rematch();
        }
    }

	public void Start()
	{
        _rematchCanvas = gameObject.transform.GetChild(0).gameObject;

        _rematchCanvas.SetActive(false);

        GameObject.Find("Player " + Constants.LOCAL_PLAYER_ID).GetComponent<NetworkPlayerInput>().RequestOwnership(gameObject.GetComponent<NetworkIdentity>());
	}

	public override void OnStartServer()
	{
        gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(GameObject.Find("Player " + Constants.LOCAL_PLAYER_ID).GetComponent<NetworkIdentity>().connectionToClient);
	}

	public void OpenScreen(bool wasWinner)
    {
        _rematchCounter = 0;
        _rematchCanvas.SetActive(true);
        if(wasWinner)
        {
            _outcomeText.text = "You won!";
        }
        else
        {
            _outcomeText.text = "You lost!";
        }
    }

    public void OnRematchPressed()
    {
        _rematchCanvas.SetActive(false);
        if (isClient)
        {
            CmdRematchPressed();
        }
        else
        {
            _rematchCounter++; 
        }
    }

    [Command]
    public void CmdRematchPressed()
    {
        _rematchCounter++;
    }

    public void OnExitPressed()
    {
        // Tell overlord that exit has been pressed
        Debug.LogError("Exit Pressed!");
    }
}
