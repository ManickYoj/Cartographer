﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;



public class Inventory : MonoBehaviour {
	public RectTransform inventoryGUIPrefab;
	public int columns;
	public int rows;

	private Item[] slots;
	private RectTransform inventoryGUI;

	
	public void Start() {
		this.slots = new Item [columns * rows];
		inventoryGUI = (RectTransform) Instantiate (inventoryGUIPrefab);
		inventoryGUI.SetParent(GameObject.FindGameObjectWithTag ("Primary Canvas").transform, false);
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