using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryGUIController : MonoBehaviour {
	public int cellPadding;

	private RectTransform slotGUIPrefab = Resources.Load<GameObject>("Prefabs/GUI/Inventory Slot").GetComponent<RectTransform>();
	private RectTransform[] slotGUIs;
	private Inventory inv;
	static InventoryVars invVars;

	public void Start() {
		if (!invVars) invVars = GameObject.FindGameObjectWithTag("Global Vars").GetComponent<InventoryVars>();
	}

	public void Initilize (Inventory inv) {
		this.inv = inv;

		// Calculate size for self and resize accordingly
		Vector2 s = slotGUIPrefab.rect.size;
		float w = (s.x + cellPadding) * inv.columns + 2 * cellPadding;
		float h = (s.y + cellPadding) * inv.rows + 2 * cellPadding;
		GetComponent<RectTransform> ().sizeDelta = new Vector2 (w, h);

		// Instantiate slot GUI elements and insert them into self
		slotGUIs = new RectTransform[inv.columns * inv.rows];
		for (int i = 0; i < inv.columns * inv.rows; i++) {
			slotGUIs[i] = (RectTransform) Instantiate(slotGUIPrefab);
			slotGUIs[i].SetParent(transform);

			int value = i;
			Button button = slotGUIs[i].GetComponent<Button>();
			if (inv.peek(i) && inv.peek(i).icon) {
				button.GetComponent<Image>().sprite = inv.peek(i).icon;
			}
			button.onClick.AddListener(() => {select(value);});
		}
	}

	public void select (int index) {
		invVars.selectItem (inv, index);
	}
}
