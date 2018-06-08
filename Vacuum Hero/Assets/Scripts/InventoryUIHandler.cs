using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHandler : MonoBehaviour {

	private bool invEnabled = false;
	private RectTransform invHandler;

	// Use this for initialization
	void Start () {
		invHandler = GameObject.Find ("Inventory").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (invEnabled) {
			invHandler.GetComponent<Image> ().enabled = true;
			if (invHandler.localPosition.y <= -290) {
				invHandler.localPosition = new Vector3 (invHandler.localPosition.x, invHandler.localPosition.y + 80, invHandler.localPosition.z);
			}
		} else {
			if (invHandler.localPosition.y >= -2200) {
				invHandler.localPosition = new Vector3 (invHandler.localPosition.x, invHandler.localPosition.y - 80, invHandler.localPosition.z);
			} else {
				invHandler.GetComponent<Image> ().enabled = false;
			}
		}

		if(invEnabled && Input.GetMouseButtonDown(0) && invHandler.gameObject.activeSelf && !RectTransformUtility.RectangleContainsScreenPoint(invHandler, Input.mousePosition, null)) {
			closeInv ();
		}
	}

	public void openInv() {
		invEnabled = true;
	}

	public void closeInv() {
		invEnabled = false;
	}
}
