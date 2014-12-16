using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Draggable : MonoBehaviour {
	Vector3 transformOffset;

	public void BeginDrag() {
		transformOffset =  Input.mousePosition - transform.position; 
		Debug.Log (transform.position);
		Debug.Log (Input.mousePosition);
		Debug.Log (transformOffset);
	}

	public void Drag() {
		transform.position = Input.mousePosition - transformOffset;
		//transform.position = transformOffset + Input.mousePosition;
	}
}
