using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ContainerInventory : MonoBehaviour, IInventory {
	public float space = 100;
	float occupiedSpace = 0;
	Dictionary<ItemData, int> contents = new Dictionary<ItemData, int>();
	public Dictionary<ItemData, int> Contents { get {return contents; } } 

	public int Add(ItemData item, int number) {
		// Add up to the number which the space allows
		int numAdded = Mathf.Min((int) Mathf.Floor((space - occupiedSpace) / item.bulk), number);
		if (contents.ContainsKey(item)) contents[item] += numAdded;
		else contents[item] = numAdded;

		// Increment the amount of space occupied
		occupiedSpace += numAdded * item.bulk;
		return numAdded;
	}

	public int Check(ItemData item) {
		if (contents.ContainsKey(item)) return contents[item];
		else return 0;
	}

	public int Retrieve(ItemData item, int number) {
		if (!contents.ContainsKey(item)) return 0;

		// If the number requested will exceed the stored amount, or bring it to 0,
		// then delete the key, and retrieve only as many as are stored
		if (number >= contents[item]) {
			number = contents[item];
			contents.Remove(item);
		}

		occupiedSpace -= number * item.bulk;
		contents [item] -= number;
		return number;
	}
}
