using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	NetworkView pNetworkView;
	float force = 20;
	float torque = 50;

	public GameObject target;
	Transform destPos;

	// Use this for initialization
	void Start () {
		destPos = target.transform;
		Name ();
	}

	void Name () {
		string[] fullID = GetComponent<NetworkView> ().viewID.ToString ().Split (new char[] {' '});
		name = "NPC " + fullID[fullID.Length-1];
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void ChangeDest (Transform pos) {
		destPos = pos;
	}

	float DeltaTheta() {
		return Vector3.Angle(transform.rotation.eulerAngles, destPos.position - transform.position);
	}

	void Move () {

		if (destPos) {

//			while(DeltaTheta() > 0) {
//				rigidbody2D.AddTorque (torque * Time.deltaTime);
//			}

			transform.LookAt(target.transform);

			//while(transform.position != destPos.positon) { 
				//rigidbody2D.AddForce(force * Time.deltaTime);
			//}
		}
	}
}