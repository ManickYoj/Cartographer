using UnityEngine;
using System.Collections;

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
	public Item[] reagents;
	public GameObject[] producedBy;
}