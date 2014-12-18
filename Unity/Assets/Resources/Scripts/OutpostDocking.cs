using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OutpostDocking : MonoBehaviour {
	public ContainerInventory salesInventory;
	public ProxyBuyer purchaseInventory;
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.name.Contains ("Player")) { // Jank much?
			AbstractInventory playerInv = other.GetComponent<AbstractInventory>();
			OutpostWindow.ActiveWindow.Display(playerInv, salesInventory, purchaseInventory);
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (OutpostWindow.ActiveWindow.gameObject.activeSelf) OutpostWindow.ActiveWindow.gameObject.SetActive(false);
	}
}
