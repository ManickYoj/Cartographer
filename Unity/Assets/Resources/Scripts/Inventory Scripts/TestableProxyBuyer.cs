using UnityEngine;
using System.Collections;

public class TestableProxyBuyer : ProxyBuyer {

	public ItemSet items;

	void Start () {
		Set (items.set [0], 5);
	}
}
