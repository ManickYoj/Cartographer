using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WindowScript : MonoBehaviour {

	RectTransform selfTransform;
	Transform contentPane;
	Text handleText;

	void Awake () {
		selfTransform = gameObject.GetComponent<RectTransform> ();
		contentPane = transform.FindChild ("Content Pane");
		handleText = transform.FindChild ("Handle/Window Name").GetComponent<Text>();
	}

	public void Wrap (RectTransform contents, string contentName) {
		selfTransform.transform.position = contents.position;
		Vector2 contentSize = contents.rect.size;
		contentSize.y += 15;

		contents.SetParent (contentPane, false);
		contents.pivot = new Vector2 (.5f, 1f);
		contents.anchoredPosition = Vector2.zero;
		selfTransform.sizeDelta = contentSize;

		handleText.text = contentName;
	}
}
