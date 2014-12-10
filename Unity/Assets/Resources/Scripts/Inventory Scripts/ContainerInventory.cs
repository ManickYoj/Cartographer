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
		int numAdded = Mathf.Min((int) Mathf.Floor((space - occupiedSpace) / item.bulk), number);
		if (contents.ContainsKey(item)) contents[item] += numAdded;
		else contents[item] = numAdded;
		occupiedSpace += numAdded * item.bulk;
		return numAdded;
	}

	public int Check(ItemData item) {
		if (contents.ContainsKey(item)) return contents[item];
		else return 0;
	}

	public int Retrieve(ItemData item, int number) {
		if (!contents.ContainsKey(item)) return 0;
		if (number > contents[item]) number = contents[item];
		contents [item] -= number;
		return number;
	}
}
