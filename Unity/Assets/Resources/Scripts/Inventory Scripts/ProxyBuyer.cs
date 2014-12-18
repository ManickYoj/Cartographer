using UnityEngine;
using System.Collections;

public class ProxyBuyer : AbstractInventory {
	public ContainerInventory underwritingInventory;

	public override int Add(ItemData item, int number) {
		if (!contents.ContainsKey(item)) return 0;

		// Prevent number purchased from exceeding the available space in underwritingInv
		number = Mathf.Min((int) Mathf.Floor(underwritingInventory.RemainingSpace () / item.bulk), number);
		
		// Prevent number purchased from exceeding the number demanded
		if (number >= contents[item]) {
			number = contents[item];
			contents.Remove(item);
			if (fullRefresh != null) fullRefresh();
		} else {
			contents [item] -= number;
			if (refreshItem != null) refreshItem(item, contents[item]);
		}

		underwritingInventory.Add (item, number);
		
		return number;
	}
	
	public override int Retrieve(ItemData item, int number) {
		return 0;
	}

	public void Set (ItemData item, int count) {
		if (!contents.ContainsKey (item)) contents.Add (item, count);
		else contents [item] = count;
	}
}
