using UnityEngine;
using System.Collections;

public class WindowThis : MonoBehaviour {

	GameObject window;
	WindowScript windowScript;
	RectTransform selfTransform;

	// Use this for initialization
	void Start () {
		initilize ();
		RefreshWindow ();
	}

	public void RefreshWindow() {
		windowScript.Wrap (selfTransform, gameObject.name); 
	}

	public void toggleWindow() {
		initilize ();
		window.SetActive (!window.activeSelf);
		RefreshWindow ();
	}

	void initilize() {
		if (!window){
			window = (GameObject)Instantiate (Resources.Load ("Prefabs/GUI/Window"));
			windowScript = window.GetComponent<WindowScript> ();
			selfTransform = gameObject.GetComponent<RectTransform> ();
			window.transform.SetParent (transform.parent, false);
		}
	}
}
