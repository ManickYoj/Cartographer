using UnityEngine;
using System.Collections;

public abstract class AbstractWindow : MonoBehaviour {
	void OnDisable () {
		UnlinkAll ();
		MouseSelection.ActiveSelection.Deselect ();
	}

	protected abstract void UnlinkAll ();
}
