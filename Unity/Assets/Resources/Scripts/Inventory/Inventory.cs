using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public delegate void InvUpdate(int index);

public class Inventory : MonoBehaviour {
	public int columns;
	public int rows;
	public Item[] startItems;

	static Item noneType;
	public event InvUpdate onInvUpdate;
	private Item[] slots;
	//private InventoryGUIController inventoryGUIScript;
	InventoryWindow  invWin;
	
	public void Start() {
		if (!noneType) noneType = Resources.Load<Item>("Prefabs/Items/None");

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

	public void ToggleGUI() {
		invWin.ToggleGUI ();
		if (invWin.gameObject.activeSelf) invWin.Reposition (Input.mousePosition);
	}

	public Item peek (int index) { return slots [index]; }

	/// <summary> Adds an item to the inventory. </summary>
	/// <param name="addition">The item to add. </param>
	/// <param name="index">The index at which to add an item.</param>
	/// <returns> True if the item was added, false otherwise. </returns>
	public bool add(Item addition, int index) {
		if (index >= 0 && index < slots.Length) {
			slots[index] = addition;
			onInvUpdate(index);
			return true;
		}

		return false;
	}

	// add() overloads for adding without specifying index, and for specifying an x, y in inventory
	public bool add(Item addition) { return add (addition, ArrayUtility.IndexOf (slots, null)); }
	public bool add(Item addition, int x, int y) { return add (addition, index (x, y)); }

	/// <summary> Removes an item from the specified index. </summary>
	/// <param name="index">The index from which to remove the item</param>
	/// <returns>The item at the index, if it exists. Null otherwise. </returns>
	public Item remove (int index) {
		Item temp = slots [index];
		slots [index] = noneType;
		onInvUpdate(index);
		return temp;
	}

	public Item remove (int x, int y) { return remove(index(x, y)); } // Removes item from inventory
	private int index (int x, int y) { return x + y * columns;  } // Parses (x, y) -> index
}