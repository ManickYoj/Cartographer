using UnityEngine;
using System.Collections;

public class InventoryVars : MonoBehaviour {
	
	private Item selected = null;


	public void selectItem (Inventory inv, int index) {
		Item temp = inv.remove (index);
		inv.add (selected, index);
		selected = temp;
		Debug.Log (selected.name);
	}

	public void clearSelection () {
		selected = null;
	}
}
