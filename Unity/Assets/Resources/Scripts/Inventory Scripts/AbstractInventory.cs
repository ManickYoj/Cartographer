﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractInventory : MonoBehaviour {
	protected Dictionary<ItemData, int> contents = new Dictionary<ItemData, int>();
	public Dictionary<ItemData, int> Contents { get {return contents; } } 

	static ItemData selected;
	static int numSelected;
	
	public delegate void Refresh (ItemData item, int count);
	public delegate void FullRefresh ();
	
	protected Refresh refreshItem;
	protected FullRefresh fullRefresh;
	
	public void Link (Refresh refreshItemFunction, FullRefresh fullRefreshFunction) {
		refreshItem = refreshItemFunction;
		fullRefresh = fullRefreshFunction;
	}

	public void Unlink () {
		refreshItem = null;
		fullRefresh = null;
	}

	public int Check(ItemData item) {
		if (contents.ContainsKey(item)) return contents[item];
		else return 0;
	}

	public abstract int Add (ItemData item, int number);
	public abstract int Retrieve (ItemData item, int number);
}
