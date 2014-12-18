using UnityEngine;
using System.Collections;

public class OutpostWindow : MonoBehaviour {
	public static OutpostWindow ActiveWindow { get; private set; }

	public InventoryDisplay playerDisplay;
	public InventoryDisplay salesDisplay;
	public InventoryDisplay purchasesDisplay;

	void Start () {
		OutpostWindow.ActiveWindow = this;
		gameObject.SetActive (false);
	}

	public void Display (AbstractInventory playerInv, AbstractInventory salesInv, ProxyBuyer purchasesAgent) {
		gameObject.SetActive (true);
		playerDisplay.Link (playerInv);
		salesDisplay.Link (salesInv);
		purchasesDisplay.Link (purchasesAgent);
	}

	void UnlinkAll() {
		playerDisplay.Unlink ();
		salesDisplay.Unlink ();
		purchasesDisplay.Unlink ();
	}

	void OnDisable () {
		UnlinkAll ();
		MouseSelection.ActiveSelection.Deselect ();
	}
}
