using UnityEngine;
using System.Collections;

public class ContainerWindow : AbstractWindow {
	public static ContainerWindow ActiveWindow { get; private set; }

	public InventoryDisplay containerDisplay;

	void Start () {
		ContainerWindow.ActiveWindow = this;
		gameObject.SetActive (false);
	}
	
	public void Display (ContainerInventory containerInv) {
		gameObject.SetActive (true);
		containerDisplay.Link (containerInv);
	}
	
	protected override void UnlinkAll() {
		containerDisplay.Unlink ();
	}
}
