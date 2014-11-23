using UnityEngine;
using System.Collections;

public class InventoryVars : MonoBehaviour {
	
	private Inventory inv = null;
	private int invIndex;


	public void selectItem (Inventory inv, int invIndex) {
		this.inv = inv;
		this.invIndex = invIndex;
	}

	public void deselectItem () {
		inv = null;
	}

	public void depositItem (Inventory targetInv, int targetInd) {
		targetInv.add (inv.remove (invIndex), targetInd);
	}
}
