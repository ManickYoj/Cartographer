using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DispIP : MonoBehaviour {
	public void Awake () {
		GetComponent<Text> ().text = Network.player.ipAddress;
	}
}
