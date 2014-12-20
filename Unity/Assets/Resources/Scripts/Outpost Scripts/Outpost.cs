using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Outpost : MonoBehaviour {
	public ContainerInventory mainInventory;
	public ContainerInventory salesInventory;
	public ProxyBuyer purchaseInventory;

	public ItemSet products;
	protected Worker[] workers;
	protected int numWorkers = 0;
	Dictionary<ItemData, float> tasks = new Dictionary<ItemData, float>();
	Dictionary<ItemData, float> reagentUse = new Dictionary<ItemData, float>();

	void OnTriggerEnter2D (Collider2D other) {
		if (other.name.Contains ("Player") && other.GetComponent<NetworkView>().isMine) { // Jank much?
			AbstractInventory playerInv = other.GetComponent<AbstractInventory>();
			OutpostWindow.ActiveWindow.Display(playerInv, salesInventory, purchaseInventory);
		}
	}
	
	void OnTriggerExit2D (Collider2D other) {
		if (OutpostWindow.ActiveWindow.gameObject.activeSelf) OutpostWindow.ActiveWindow.gameObject.SetActive(false);
	}

	void OnMouseDown() {
		if (ContainerWindow.ActiveWindow.gameObject.activeSelf)
			ContainerWindow.ActiveWindow.gameObject.SetActive(false);
		else ContainerWindow.ActiveWindow.Display(mainInventory);
	}

	void Update () {
		Produce ();
	}

	protected void Produce() {
		for (int i = 0; i < numWorkers; i++ ) {
			ItemData item = workers[i].Task;
			if (item != null && UseReagents(item)) {

				tasks [item] +=  workers[i].Work() * Time.deltaTime;

				if ( tasks [item] >= item.laborCost ) {
					mainInventory.Add(item, Random.Range(item.producedMin, item.producedMax + 1));
					tasks [item] -= item.laborCost;
				}
			}
		}
	}

	bool UseReagents (ItemData product) {
		ReagentCost[] reagents = product.reagents;

		// Check to ensure inventory has all reagents before progressing
		foreach (ReagentCost r in reagents) 
			if (!(mainInventory.Check(r.reagent) > r.number)) return false;

		// Increment usage of a reagent towards 1 and, if it passes 1, remove those reagents
		// from the inventory (implicitly destroying them)
		foreach (ReagentCost r in reagents) {
			if (!reagentUse.ContainsKey(r.reagent)) reagentUse.Add(r.reagent, 0f);
			reagentUse[r.reagent] += r.number * Time.deltaTime;
			if (reagentUse[r.reagent] >= 1) {
				mainInventory.Retrieve(r.reagent, Mathf.FloorToInt(reagentUse[r.reagent]));
				reagentUse[r.reagent] = 0f;
			}
		}

		return true;
	}

	public bool AddWorker(Worker w) {
		if (numWorkers >= workers.Length) return false;
		else {
			workers [numWorkers] = w;
			numWorkers ++;
			return true;
		}
	}

	protected void AssignWorker(Worker w, ItemData task) {
		if (!tasks.ContainsKey(task)) tasks.Add(task, 0.0f);
		w.Task = task;
	}
}
