using UnityEngine;
using System.Collections;

public class TestableInventory : ContainerInventory {
	public ItemSet testSet;
	public InventoryDisplay testDisplay;

	// Use this for initialization
	void Start () {
		PopulateInventory ();
		testDisplay.Link (this);
	}

	void PopulateInventory () {
		while (space - occupiedSpace > 10) {
			int index = Random.Range(0, testSet.set.Length);
			Add(testSet.set[index], 1);
		}
	}
}
