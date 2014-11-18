using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {
	public Text hostIP;
	public int hostPort = 25001;

	public GameObject preconnectionMenu;
	public GameObject postconnectionMenu;

	void Start () {
		// Set up menus
		preconnectionMenu.SetActive (true);
		postconnectionMenu.SetActive (false);
		hostIP.text = Network.player.ipAddress;
	}

	public void Join() {
		Debug.Log ("Attempting to join server at " + hostIP.text);
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
	}

	public void Disconnect() {
		Network.Disconnect (200);

		// Reset Menus
		preconnectionMenu.SetActive (true);
		postconnectionMenu.SetActive (false);
	}

	void UpdateConnectionStatus(){
		//Swap Menus
		preconnectionMenu.SetActive (false);
		postconnectionMenu.SetActive (true);
	}
}
