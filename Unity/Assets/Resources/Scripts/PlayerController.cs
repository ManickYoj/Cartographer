using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	NetworkView pNetworkView;
	float force = 20;
	float torque = 50;

	Camera camera;
	public Camera cameraPrefab;

	void Start () {

		pNetworkView = GetComponent<NetworkView> ();

		// Instantiate a unique camera for the player
		AssignCamera ();

		// Name player and camera based on their viewID
		Name ();
	}

	void Update () {
		Move ();
	}

	void Name () {
		string[] fullID = GetComponent<NetworkView> ().viewID.ToString ().Split (new char[] {' '});
		name = "Player " + fullID[fullID.Length-1];
		camera.name = "Camera " + fullID[fullID.Length-1];
	}

	void AssignCamera () {
		if (pNetworkView.isMine) {
			camera = (Camera) Instantiate (cameraPrefab, cameraPrefab.transform.position, cameraPrefab.transform.rotation);
			camera.gameObject.SetActive (true);
			camera.gameObject.tag = "MainCamera";
			camera.GetComponent<CameraController> ().Follow (this.transform);
		}
	}

	void Move () {
		if (pNetworkView.isMine) {
			Vector2 vel = Input.GetAxis ("Vertical") * transform.up;
			float rot = - Input.GetAxis ("Horizontal");

			rigidbody2D.AddForce (vel * force * Time.deltaTime);
			rigidbody2D.AddTorque (rot * torque * Time.deltaTime);
		}
	}
}