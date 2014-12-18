using UnityEngine;
using System.Collections;

public class SalesDisplay : InventoryDisplay {

	override public void AddSelectedItem () {
		MouseSelection.ActiveSelection.Deselect ();
	}
}
