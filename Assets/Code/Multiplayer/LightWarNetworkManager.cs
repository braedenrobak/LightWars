using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LightWarNetworkManager : NetworkManager {

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        GameObject.Find("NetworkMain").GetComponent<NetworkMain>().AddPlayer(player);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
