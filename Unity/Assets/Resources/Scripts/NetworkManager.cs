using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {
	public Text hostIP;
	public int hostPort = 25001;

	public GameObject preconnectionMenu;
	public GameObject postconnectionMenu;

	public GameObject playerPrefab;
	GameObject player;

	void Start () {
		// Set up menus
		preconnectionMenu.SetActive (true);
		postconnectionMenu.SetActive (false);
		hostIP.text = Network.player.ipAddress;
	}

	public void Join() {
		Network.Connect (hostIP.text, hostPort);
		UpdateConnectionStatus ();
	}

	public void Host() {
		// Host Variables
		int maxPlayers = 32;
		bool natPunchthrough = false;

		// Create Server
		Network.InitializeServer(maxPlayers, hostPort, natPunchthrough);
		UpdateConnectionStatus ();
		CreatePlayer ();
	}

	void OnConnectedToServer() {
		Debug.Log ("Connected to Server.");
		CreatePlayer ();
	}

	void OnDisconnectedFromServer(NetworkDisconnection e) {
		if (Network.isServer) Debug.Log("Client Disconnected."); 
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Clean up after player " + player);

		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
		Network.CloseConnection (player, true);
	}

	void OnPlayerConnected(NetworkPlayer player) {
		foreach(NetworkPlayer c in Network.connections) {
			Debug.Log (c);
		}
	}
	
	public void Disconnect() {
		Network.Destroy (player.GetComponent <NetworkView> ().viewID);
		Network.Disconnect (200);
		//if (Network.isClient) Network.CloseConnection (Network.connections[0], true);

		// Reset Menus
		preconnectionMenu.SetActive (true);
		postconnectionMenu.SetActive (false);
	}

	void UpdateConnectionStatus(){
		//Swap Menus
		preconnectionMenu.SetActive (false);
		postconnectionMenu.SetActive (true);
	}

	void CreatePlayer() {
		player = (GameObject) Network.Instantiate (playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation, 0);
		player.name = "Player";
		player.transform.Find ("Camera").gameObject.SetActive(true);
	}
}