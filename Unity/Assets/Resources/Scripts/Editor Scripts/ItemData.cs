using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemData : ScriptableObject {
	//General Data
	public Sprite icon;
	public int baseValue;
	public float bulk;

	//Market Data
	public float defaultNumberPopMultiplier;
	public int defaultNumberPopCutoff;
	
	// Production Data
	public int laborCost;
	public ReagentCost[] reagents;
	public int producedMin = 1;
	public int producedMax = 1;
}