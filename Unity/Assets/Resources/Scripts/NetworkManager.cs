using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {
	public Text hostIP;
	public int hostPort = 25001;

	public GameObject preconnectionMenu;
	public GameObject postconnectionMenu;

	GameObject player;
	public GameObject playerPrefab;

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
		CreatePlayer ();
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		Network.RemoveRPCs(player);
	}

	public void Disconnect() {

		Network.Destroy (player);
		Network.Disconnect (200);

		// Destroy the player's main camera
		Destroy (Camera.main.gameObject);

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
	}
}