using UnityEngine;
using System.Collections;

public class WaypointModule : MonoBehaviour {

	GameObject[] buildings;
	GameObject nextWaypoint;
	bool atWaypoint = true;
	
	// Use this for initialization
	void Start () {
		buildings = GameObject.FindGameObjectsWithTag ("Building");
	}
	
	// Update is called once per frame
	void Update () {

		if (nextWaypoint != null) {

			Vector3 tTarget = new Vector3(nextWaypoint.transform.position.x, nextWaypoint.transform.position.y, 0);
			Vector3 tSelf = new Vector3(transform.position.x, transform.position.y, 0);
			if(Vector3.Distance(tTarget, tSelf) < 0.5) {
				atWaypoint = true;
			}
		}
		if (atWaypoint) {
			nextWaypoint = buildings[Random.Range(0, buildings.Length)];
			GetComponent<AIController>().SetTarget(nextWaypoint);
			atWaypoint = false;
		}

	}
}