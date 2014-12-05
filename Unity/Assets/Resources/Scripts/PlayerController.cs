using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	NetworkView pNetworkView;

	void Start () {
		pNetworkView = GetComponent<NetworkView> ();
	}

	// Update is called once per frame
	void Update () {
		if (pNetworkView.isMine){
			Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
			float speed = 5;
			transform.Translate(speed * moveDir * Time.deltaTime);
		}
	}
}