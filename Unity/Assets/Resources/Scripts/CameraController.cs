using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
		Transform playerTransform;

		// Update is called once per frame
		void LateUpdate ()	{
			if (playerTransform) {
				float distanceAway = 3;
				Vector3 pos = playerTransform.position;
				transform.position = new Vector3 (pos.x, pos.y - distanceAway, pos.z - distanceAway);
			}
		}

		public void Follow (Transform t) {
			playerTransform = t;
		}
}