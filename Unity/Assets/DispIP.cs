using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DispIP : MonoBehaviour {
	public void Awake () {
		Debug.Log (Network.player.externalIP);
		Debug.Log (Network.player);
		GetComponent<Text> ().text = Network.player.externalIP;
	}
}
