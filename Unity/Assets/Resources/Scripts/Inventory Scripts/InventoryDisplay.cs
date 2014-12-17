using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour {

	public Button itemDisplay;

	AbstractInventory linkedInventory;
	Button[] itemDisplays;
	Dictionary<ItemData, int> buttonMap = new Dictionary<ItemData, int>();
	int lastButtonIndex = 0;
	static MouseSelection selection;

	void Awake () {
		if (!selection) selection = GameObject.FindGameObjectWithTag ("MouseSelection").GetComponent<MouseSelection>();
		// Create item displays
		Vector2 windowSize = GetComponent<RectTransform>().rect.size;
		GridLayoutGroup layoutGroup = GetComponent<GridLayoutGroup> ();

		int numCols = Mathf.FloorToInt ((windowSize.x - layoutGroup.padding.horizontal) / (layoutGroup.cellSize.x + layoutGroup.spacing.x));
		int numRows = Mathf.FloorToInt ((windowSize.y - layoutGroup.padding.vertical) / (layoutGroup.cellSize.y + layoutGroup.spacing.y));
		itemDisplays = new Button [numCols * numRows];

		for (int i = 0; i < numCols*numRows; i++) createNewButton(i);
	}

	void OnDisable () { if (linkedInventory != null) linkedInventory.Link (null, null); }
	void OnEnable () { if (linkedInventory != null) FullRefresh(); }

	void createNewButton (int index) {
		// Creates a new button and links it's activation with the activate(index) function
		itemDisplays [index] = (Button) Button.Instantiate(itemDisplay);
		itemDisplays [index].transform.SetParent(transform, false);
		itemDisplays [index].gameObject.SetActive (false);
	}

	/// <summary>
	/// Link the specified inventory to this display, or refresh it when a new content category is added or removed.
	/// </summary>
	/// <param name="inventory">The inventory to link.</param>
	public void Link(AbstractInventory inventory) {
		linkedInventory = inventory;
		lastButtonIndex = 0;
		foreach (ItemData item in inventory.Contents.Keys){
			ListItem(item, lastButtonIndex);
			lastButtonIndex++;
		}
		Clear ();
		inventory.Link (Refresh, FullRefresh);
	}

	void Refresh (ItemData item, int count) {
		// Refresh display of the indexed item (if it goes to zero)
		itemDisplays [buttonMap [item]].GetComponentInChildren<Text> ().text = count.ToString();
	}

	void FullRefresh() { Link (linkedInventory); }

	void Clear() {
		// Disable all slots after last button
		for (int i = lastButtonIndex; i < itemDisplays.Length; i++) itemDisplays[i].gameObject.SetActive(false);
	}

	void ListItem (ItemData item, int buttonIndex) {
		itemDisplays [buttonIndex].onClick.AddListener( delegate { selection.Select (item, linkedInventory); } );
		itemDisplays [buttonIndex].gameObject.SetActive (true);
		itemDisplays [buttonIndex].GetComponent<Image> ().sprite = item.icon;
		buttonMap [item] = buttonIndex;
		Refresh (item, linkedInventory.Contents [item]);
	}

	public void AddItem (ItemData item, int count) {
		linkedInventory.Add (item, count);
	}
}
