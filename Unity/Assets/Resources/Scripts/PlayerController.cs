using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Network.isServer){
			Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
			float speed = 5;
			transform.Translate(speed * moveDir * Time.deltaTime);

			if (Input.GetButtonDown("Inventory")){
				GetComponent<Inventory>().ToggleGUI();
			}
		}
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		if (stream.isWriting){ 
			Vector3 pos = transform.position;
			stream.Serialize(ref pos);
		} else {
			Vector3 receivedPosition = Vector3.zero;
			stream.Serialize(ref receivedPosition);
			transform.position = receivedPosition;
		}
	}
}
