using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PurchasesDisplay : InventoryDisplay {
	override protected void ListItem (ItemData item, int buttonIndex) {
		itemDisplays [buttonIndex].gameObject.SetActive (true);
		itemDisplays [buttonIndex].GetComponent<Image> ().sprite = item.icon;
		buttonMap [item] = buttonIndex;
		Refresh (item, linkedInventory.Contents [item]);
	}
}
