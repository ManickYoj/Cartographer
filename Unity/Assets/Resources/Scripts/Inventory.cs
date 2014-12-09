using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void InvUpdate(int index);

public class Inventory : MonoBehaviour {
	static Item selected;
	static Item noneType;
	static Inventory lastInventory;
	static int lastIndex;

	public int columns;
	public int rows;
	public Item[] startItems;

	public event InvUpdate onInvUpdate;
	public Item[] slots;
	NetworkView pNetworkView;
	InventoryWindow  invWin;
	
	void Start() {
		if (!noneType) noneType = Resources.Load<Item>("Prefabs/Items/None");
		if (!selected) selected = noneType;
		pNetworkView = GetComponent<NetworkView> ();

		// Initilize class variables
		this.slots = new Item [columns * rows];
		for (int i = 0; i < slots.Length; i++) {
			if ( i < startItems.Length) slots[i] = startItems[i];
			else slots[i] = noneType;
		}

		// Create & initilize associated GUI element
		GameObject inventoryGUI = (GameObject) Instantiate (Resources.Load ("Prefabs/GUI/Inventory Window"));
		inventoryGUI.transform.SetParent(GameObject.FindGameObjectWithTag ("Primary Canvas").transform, false);
		invWin = inventoryGUI.GetComponent<InventoryWindow> ();
		invWin.Init(this);
		ToggleGUI ();
	}

	void Update() {
		if (Input.GetButtonDown("Inventory") && pNetworkView && pNetworkView.isMine) ToggleGUI();
	}

	public void ToggleGUI() {
		invWin.ToggleGUI ();
		if (invWin.gameObject.activeSelf) invWin.Reposition (Input.mousePosition);
	}

	public Item peek (int index) { return slots [index]; }

	public void SelectItem (int index) {
		if (selected == noneType) {
			if (slots[index] != noneType) {
				selected = slots[index];
				slots [index] = noneType;
				lastIndex = index;
				lastInventory = this;
			}
		} else {
			if (slots[index] == noneType) {
				slots[index] = selected;
				selected = noneType;
			} else {
				lastInventory.slots [lastIndex] = selected;
				selected = noneType;
				onInvUpdate(lastIndex);
			}
		}

		onInvUpdate (index);
	}
}