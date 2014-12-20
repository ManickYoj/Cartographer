using UnityEngine;
using System.Collections;

public class TestableProxyBuyer : ProxyBuyer {

	public ItemSet items;

	void Start () {
		foreach (ItemData i in items.set) if (i.name == "Mining Tools") Set (i, 10);
	}
}
