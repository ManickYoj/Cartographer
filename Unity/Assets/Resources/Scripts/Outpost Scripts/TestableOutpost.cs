using UnityEngine;
using System.Collections;

public class TestableOutpost : Outpost {
	void Start () {
		workers = new Worker[3];
		AddWorker (new Worker (1));
		AddWorker (new Worker (1));
		AddWorker (new Worker (1));

		AssignWorker (workers[0], products.set [0]);
		AssignWorker (workers[1], products.set [0]);
		AssignWorker (workers[2], products.set [1]);
	}
}
