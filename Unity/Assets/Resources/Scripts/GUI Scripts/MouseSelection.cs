using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseSelection : MonoBehaviour {
	public static MouseSelection ActiveSelection { get; private set; }

	ItemData item;
	AbstractInventory inventory;
	int count;
	
	Vector3 adjustmentVector = new Vector3 (1, 1);
	Image image;
	Sprite defaultSprite;

	void Start() {
		MouseSelection.ActiveSelection = this;
		image = GetComponent<Image> ();
		defaultSprite = image.sprite;
	}

	void Update () { transform.position = Input.mousePosition + adjustmentVector; }

	public void Select(ItemData item, AbstractInventory inventory) {
		Deselect ();

		image.sprite = item.icon;
		this.item = item;
		this.inventory = inventory;
		count = inventory.Retrieve (item, 1);
		Screen.showCursor = false;
	}

	public void Deselect () {
		if (inventory == null) return;
		inventory.Add (item, count);
		NullifySelection();
	}

	public void InventoryClick (InventoryDisplay inventory) {
		if (this.inventory == null ) return;
		if (inventory.AddItem (item, count) != 0) NullifySelection ();
	}

	void NullifySelection () {
		item = null;
		inventory = null;
		count = 0;
		image.sprite = defaultSprite;
		Screen.showCursor = true;
	}
}
