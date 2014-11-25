using UnityEngine;
using System.Collections;

public class InventoryVars : MonoBehaviour {
	
	static Item selected;

	void Start() { clearSelection (); }


	public void selectItem (Inventory inv, int index) {
		Item temp = inv.remove (index);
		inv.add (selected, index);
		selected = temp;
	}

	public void clearSelection () {
		selected = Resources.Load<Item> ("Prefabs/Items/None");
	}
}
