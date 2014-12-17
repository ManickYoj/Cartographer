using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	NetworkView pNetworkView;
	float torque = 50;

	// PID Controller
	public Vector3 targetPos = Vector3.zero;
	float maxForce = 10f;
	float pGain = 0.5f;
	float dGain = 0.7f;
	Vector3 lastError = Vector3.zero;
	Vector3 currentPos = Vector3.zero;
	Vector3 force = Vector3.zero;

	//float iGain = 0.2f;
	//Vector3 integrator = Vector3.zero;

	bool turning = true;
	Quaternion lastRotation;
	
	// Use this for initialization
	void Start () {
		Name ();

		lastRotation = transform.rotation;
	}

	void ChangeTarget(Vector3 pos) {
		targetPos = pos;
		turning = true;
	}

	void Name () {
		string[] fullID = GetComponent<NetworkView> ().viewID.ToString ().Split (new char[] {' '});
		name = "NPC " + fullID[fullID.Length-1];
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move () {
		if (turning) {
			// Rotate toward the destination
			//Quaternion rotQuaternion = Quaternion.LookRotation(targetPos - transform.position);
			//transform.rotation = Quaternion.Slerp(transform.rotation, rotQuaternion, Time.deltaTime);
			//transform.rotation = Quaternion.Euler(new Vector3(0f,0f,transform.eulerAngles.z));

			float deltaTheta = Vector3.Angle (transform.position, targetPos);
			Quaternion rotQuaternion = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, deltaTheta));
			transform.rotation = Quaternion.Slerp(transform.rotation, rotQuaternion, Time.deltaTime * 2);

			if (transform.rotation.Equals(lastRotation)) turning = false;
			lastRotation = transform.rotation;
		}

		else {
			//Debug.Log
			// Move toward the destination
			currentPos = transform.position;
			Vector3 error = targetPos - currentPos;

			//integrator += error * Time.deltaTime;
			Vector3 diff = (error - lastError) / Time.deltaTime;
			lastError = error;

			force = error * pGain + diff * dGain; //+ integrator * iGain;
			force = Vector3.ClampMagnitude(force, maxForce);
			rigidbody2D.AddForce(force);
		}
	}
}