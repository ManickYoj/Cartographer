using UnityEngine;
using System.Collections;

public class TestableInventory : ContainerInventory {
	public ItemSet testSet;

	// Use this for initialization
	void Start () {
		PopulateInventory ();
	}

	void PopulateInventory () {
		while (space - occupiedSpace > 10) {
			int index = Random.Range(0, testSet.set.Length);
			Add(testSet.set[index], 1);
		}
	}

	public void ClickMeRemove() { Debug.Log (Retrieve (testSet.set[0], 10)); }

	public void ClickMeAdd() { Debug.Log (Add (testSet.set[0], 10)); }
}
