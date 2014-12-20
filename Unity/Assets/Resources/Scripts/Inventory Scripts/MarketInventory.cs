using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarketInventory : AbstractInventory {	
	public override int Add(ItemData item, int number) {
		// Add up to the number which the space allows
		if (contents.ContainsKey(item)) {
			contents[item] += number;
			Refresh(item, contents[item]);
		}
		else {
			contents[item] = number;
			FullRefresh();
		}

		return number;
	}
	
	public override int Retrieve(ItemData item, int number) {
		if (!contents.ContainsKey(item)) return 0;
		
		// If the number requested will exceed the stored amount, or bring it to 0,
		// then delete the key, and retrieve only as many as are stored
		if (number >= contents[item]) {
			number = contents[item];
			contents.Remove(item);
			FullRefresh();
		} else {
			contents [item] -= number;
			Refresh(item, contents[item]);
		}
		
		return number;
	}
}
