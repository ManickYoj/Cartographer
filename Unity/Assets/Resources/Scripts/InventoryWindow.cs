﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class InventoryWindow : MonoBehaviour {
	// Controller instance variables
	Inventory inv;
	static InventoryVars invVars;

	// Window instance variables
	RectTransform selfTransform;
	Transform contentPane;
	Text handleText;

	// Inventory GUI instance variables
	Button[] slotGUIs;
	public int cellPadding;

	void Start() { invVars = GameObject.FindGameObjectWithTag ("Global Vars").GetComponent<InventoryVars> (); }

	public void Init (Inventory associatedInventory) {
		// Get Instance Variables
		inv = associatedInventory;
		selfTransform = gameObject.GetComponent<RectTransform> ();
		contentPane = transform.FindChild ("Content Pane");
		handleText = transform.FindChild ("Handle/Window Name").GetComponent<Text>();

		// Register an observer with the inventory
		inv.onInvUpdate += UpdateIndex;

		createGUI ();
	}

	void createGUI () {
		// Calculate size for self and resize accordingly
		Button slotGUIPrefab = Resources.Load<Button>("Prefabs/GUI/Inventory Slot");
		Vector2 s = slotGUIPrefab.GetComponent<RectTransform>().rect.size;
		float w = (s.x + cellPadding) * inv.columns + 2 * cellPadding;
		float h = (s.y + cellPadding) * inv.rows + 2 * cellPadding;
		RectTransform contentSize = contentPane.GetComponent<RectTransform> ();
		contentSize.sizeDelta = new Vector2 (w, h);
		selfTransform.sizeDelta = contentSize.rect.size + new Vector2(0, 15);

		// Instantiate slot GUI elements and insert them into self
		slotGUIs = new Button[inv.columns * inv.rows];
		for (int i = 0; i < inv.columns * inv.rows; i++) {
			slotGUIs[i] = (Button)Instantiate(slotGUIPrefab);
			slotGUIs[i].transform.SetParent(contentPane.transform);

			int value = i;  // A bug occurs if this seemingly useless code does not exist
			slotGUIs[i].onClick.AddListener(() => { invVars.selectItem (inv, value); });
			UpdateIndex(i);
		}
	}

	public void ToggleGUI () {
		gameObject.SetActive (!gameObject.activeSelf);
		UpdateAllIndicies();
	}

	public void Reposition (Vector2 newLocation) { transform.position = newLocation; }

	void UpdateIndex(int index) { slotGUIs[index].GetComponent<Image>().sprite = inv.peek(index).icon; }
	public void UpdateAllIndicies () { for (int i = 0; i < inv.columns * inv.rows; i++) { UpdateIndex(i); } }

}
