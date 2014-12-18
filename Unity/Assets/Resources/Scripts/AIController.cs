using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	NetworkView pNetworkView;

	// PD Controller
	float maxForce = 1;
	float pGain = 0.5f;
	float dGain = 0.7f;
	Vector3 lastError = Vector3.zero;
	Vector3 currentPos = Vector3.zero;
	Vector3 force = Vector3.zero;

	bool turning = false;
	Quaternion lastRotation;

	public GameObject target;
	
	// Use this for initialization
	void Start () {
		Name ();

		lastRotation = transform.rotation;
	}

	public void SetTarget(GameObject t) {
		target = t;
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
		if (target) {
			if (turning) {
				// Rotate toward the destination
				Vector3 tTarget = new Vector3(target.transform.position.x, target.transform.position.y, 0);
				Vector3 tSelf = new Vector3(transform.position.x, transform.position.y, 0);

				float angle = Vector3.Angle(tTarget - tSelf, Vector3.up);
				Quaternion q = Quaternion.AngleAxis(angle, transform.forward);
				transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y,
				                                      Quaternion.Slerp(transform.rotation, q, Time.deltaTime).eulerAngles.z);

				if (Quaternion.Angle(transform.rotation,lastRotation) == 0) turning = false;
				lastRotation = transform.rotation;
			}

			else {
				// Move toward the destination, PD Controller
				currentPos = transform.position;
				Vector3 error = target.transform.position - currentPos;

				Vector3 diff = (error - lastError) / Time.deltaTime;
				lastError = error;

				force = error * pGain + diff * dGain;
				force = Vector3.ClampMagnitude(force, maxForce);
				rigidbody2D.AddForce(force);
			}
		}
	}
}