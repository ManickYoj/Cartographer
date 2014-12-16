using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ContainerInventory : AbstractInventory {
	public float space = 100;
	protected float occupiedSpace = 0;
	
	public override int Add(ItemData item, int number) {
		// Add up to the number which the space allows
		int numAdded = Mathf.Min((int) Mathf.Floor((space - occupiedSpace) / item.bulk), number);
		if (contents.ContainsKey(item)) {
			contents[item] += numAdded;
			if (refreshItem != null) refreshItem(item, contents[item]);
		}
		else {
			contents[item] = numAdded;
			if (fullRefresh != null) fullRefresh();
		}

		// Increment the amount of space occupied
		occupiedSpace += numAdded * item.bulk;
		return numAdded;
	}

	public override int Retrieve(ItemData item, int number) {
		if (!contents.ContainsKey(item)) return 0;

		// If the number requested will exceed the stored amount, or bring it to 0,
		// then delete the key, and retrieve only as many as are stored
		if (number >= contents[item]) {
			number = contents[item];
			contents.Remove(item);
			if (fullRefresh != null) fullRefresh();
		} else {
			contents [item] -= number;
			if (refreshItem != null) refreshItem(item, contents[item]);
		}

		occupiedSpace -= number * item.bulk;
		
		return number;
	}
}
