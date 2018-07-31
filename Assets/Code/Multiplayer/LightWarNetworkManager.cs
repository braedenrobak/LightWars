using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LightWarNetworkManager : NetworkManager {

    public GameObject _networkMain;

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        _networkMain.GetComponent<NetworkMain>().AddPlayer(player);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

	public override void OnStopServer()
	{
		base.OnStopServer();
        _networkMain.SetActive(false);
        _networkMain.GetComponent<NetworkMain>().Reset();
	}

	public override void OnStopClient()
	{
		base.OnStopClient();
        _networkMain.SetActive(false);
        _networkMain.GetComponent<NetworkMain>().Reset();
	}

	public override void OnStartClient(NetworkClient client)
	{
		base.OnStartClient(client);
        _networkMain.SetActive(true);
        _networkMain.GetComponent<NetworkMain>().Setup();
	}
}
