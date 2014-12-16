using UnityEditor;
using UnityEngine;
using System.Collections;

public class CreateItemData {
	[MenuItem("Assets/Create/Item Data Object")]
	
	public static void CreateItemDataObject () {
		ItemData asset = ScriptableObject.CreateInstance<ItemData> ();
		AssetDatabase.CreateAsset (asset, "Assets/Resources/Scriptable Objects/New Item Data.asset");
		AssetDatabase.SaveAssets ();
		
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}
}

public class CreateItemSet {
	[MenuItem("Assets/Create/Item Set Object")]
	
	public static void CreateItemDataObject () {
		ItemSet asset = ScriptableObject.CreateInstance<ItemSet> ();
		AssetDatabase.CreateAsset (asset, "Assets/Resources/Scriptable Objects/New Item Set.asset");
		AssetDatabase.SaveAssets ();
		
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}
}
