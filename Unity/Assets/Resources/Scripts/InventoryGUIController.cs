using UnityEngine;
using System.Collections;

public class InventoryGUIController : MonoBehaviour {
	public RectTransform slotGUIPrefab;
	public int cellPadding;

	private RectTransform[] slotGUIs;

	public void Initilize (int columns, int rows) {
		// Calculate size for self and resize accordingly
		Vector2 s = slotGUIPrefab.rect.size;
		float w = (s.x + cellPadding) * columns + 2 * cellPadding;
		float h = (s.y + cellPadding) * rows + 2 * cellPadding;
		GetComponent<RectTransform> ().sizeDelta = new Vector2 (w, h);


		// Instantiate slot GUI elements and insert them into self
		slotGUIs = new RectTransform[columns * rows];
		for (int i =0; i < columns * rows; i++) {
			slotGUIs[i] = (RectTransform) Instantiate(slotGUIPrefab);
			slotGUIs[i].SetParent(transform);
		}
	}
}
