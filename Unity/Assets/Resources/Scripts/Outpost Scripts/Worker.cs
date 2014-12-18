using UnityEngine;
using System.Collections;

public class Worker  {
	public float productivity { get; private set; }
	public ItemData Task;

	public float Work () { return productivity; }

	public Worker (float productivity) {
		this.productivity = productivity;
	}
}
