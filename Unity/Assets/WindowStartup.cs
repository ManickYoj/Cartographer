using UnityEngine;
using System.Collections;

public class WindowStartup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject outpostWindow = (GameObject) Instantiate (Resources.Load ("Prefabs/GUI/OutpostWindow"));
		outpostWindow.transform.SetParent (transform, false);
		GameObject containerWindow = (GameObject) Instantiate (Resources.Load ("Prefabs/GUI/Container Window"));
		containerWindow.transform.SetParent (transform, false);
	}
}
