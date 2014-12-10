using UnityEngine;
using System.Collections;

public interface IInventory {
	int Add (ItemData item, int number);
	int Check (ItemData item);
	int Retrieve (ItemData item, int number);
}
