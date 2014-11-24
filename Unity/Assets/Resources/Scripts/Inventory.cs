using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;



public class Inventory : MonoBehaviour {
	public int columns;
	public int rows;
	public Item[] startItems;

	private Item[] slots;
	private InventoryGUIController inventoryGUIScript;
	
	public void Start() {
		// Initilize class variables
		this.slots = new Item [columns * rows];
		for (int i = 0; i < slots.Length; i++) {
			if ( i < startItems.Length) slots[i] = startItems[i];
			else break;
		}

		// Create & initilize associated GUI element
		GameObject inventoryGUI = (GameObject) Instantiate (Resources.Load ("Prefabs/GUI/Inventory GUI"));
		inventoryGUI.transform.SetParent(GameObject.FindGameObjectWithTag ("Primary Canvas").transform, false);
		inventoryGUIScript = inventoryGUI.GetComponent<InventoryGUIController> ();
		inventoryGUIScript.Initilize(this);
	}


	public Item peek (int index) {
		return slots [index];
	}

	/// <summary> Adds an item to the inventory. </summary>
	/// <param name="addition">The item to add. </param>
	/// <param name="index">The index at which to add an item.</param>
	/// <returns> True if the item was added, false otherwise. </returns>
	public bool add(Item addition, int index) {
		if (index >= 0 && index < slots.Length) {
			slots[index] = addition;
			if (addition) return true;
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
		if (slots [index] != null) {
			Item temp = slots [index];
			slots [index] = null;
			return temp;
		} else return null;
	}

	public Item remove (int x, int y) { return remove(index(x, y)); } // Removes item from inventory
	private int index (int x, int y) { return x + y * columns;  } // Parses (x, y) -> index
}