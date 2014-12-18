using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Draggable : MonoBehaviour {
	Vector3 transformOffset;

	public void BeginDrag() {
		transformOffset =  Input.mousePosition - transform.position; 
	}

	public void Drag() {
		transform.position = Input.mousePosition - transformOffset;
		//transform.position = transformOffset + Input.mousePosition;
	}
}
