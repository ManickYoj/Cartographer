using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryGUIController : MonoBehaviour {
	public int cellPadding;
	
	private Button[] slotGUIs;
	private Inventory inv;
	static InventoryVars invVars;

	public void Start() {
		if (!invVars) invVars = GameObject.FindGameObjectWithTag("Global Vars").GetComponent<InventoryVars>();
	}

	public void Initilize (Inventory inv) {
		this.inv = inv;
		inv.invUpdate += updateIndex;
		Button slotGUIPrefab = Resources.Load<Button>("Prefabs/GUI/Inventory Slot");

		// Calculate size for self and resize accordingly
		Vector2 s = slotGUIPrefab.GetComponent<RectTransform>().rect.size;
		float w = (s.x + cellPadding) * inv.columns + 2 * cellPadding;
		float h = (s.y + cellPadding) * inv.rows + 2 * cellPadding;
		GetComponent<RectTransform> ().sizeDelta = new Vector2 (w, h);

		// Instantiate slot GUI elements and insert them into self
		slotGUIs = new Button[inv.columns * inv.rows];
		for (int i = 0; i < inv.columns * inv.rows; i++) {
			slotGUIs[i] = (Button)Instantiate(slotGUIPrefab);
			slotGUIs[i].transform.SetParent(transform);
			
			int value = i;
			slotGUIs[i].onClick.AddListener(() => { invVars.selectItem (inv, value); });
			updateIndex(i);
		}
	}

	public void updateAllIndicies () {
		for (int i = 0; i < inv.columns * inv.rows; i++) { updateIndex(i); }
	}

	void updateIndex(int index) {
		slotGUIs[index].GetComponent<Image>().sprite = inv.peek(index).icon;
	}
}
