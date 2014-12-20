using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractInventory : MonoBehaviour {
	protected Dictionary<ItemData, int> contents = new Dictionary<ItemData, int>();
	public Dictionary<ItemData, int> Contents { get {return contents; } } 

	static ItemData selected;
	static int numSelected;
	
	public delegate void RefreshDelegate (ItemData item, int count);
	public delegate void FullRefreshDelegate ();

	public event RefreshDelegate refreshEvent;
	public event FullRefreshDelegate fullRefreshEvent;

	protected void Refresh (ItemData item, int count) {
		if (refreshEvent != null ) refreshEvent (item, count);
	}

	protected void FullRefresh () {
		if (fullRefreshEvent != null ) fullRefreshEvent();
	}

	public int Check(ItemData item) {
		if (contents.ContainsKey(item)) return contents[item];
		else return 0;
	}

	public abstract int Add (ItemData item, int number);
	public abstract int Retrieve (ItemData item, int number);
}
