using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	public string hostIP = "127.0.0.1";	
	public int hostPort = 25001;

	public GameObject preconnectionMenu;
	public GameObject postconnectionMenu;


	void Start () {
		// Set up menus
		preconnectionMenu.SetActive (true);
		postconnectionMenu.SetActive (false);
	}

	public void Join() {
		Network.Connect (hostIP, hostPort);
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
