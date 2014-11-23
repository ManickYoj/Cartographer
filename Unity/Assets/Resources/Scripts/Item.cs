using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	//General Data
	public Sprite icon;
	public int baseValue;

	// Production Data
	public int laborCost;
	public Item[] reagents;
	public GameObject[] producedBy;
}
