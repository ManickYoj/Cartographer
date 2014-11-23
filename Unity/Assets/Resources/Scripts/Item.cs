﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour {
	//General Data
	public Image icon;
	public int baseValue;

	// Production Data
	public int laborCost;
	public Item[] reagents;
	public GameObject[] producedBy;
}
