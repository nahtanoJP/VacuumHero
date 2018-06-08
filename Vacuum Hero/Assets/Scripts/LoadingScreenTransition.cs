using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenTransition : MonoBehaviour {

	public static bool end = false, start = false;
	RectTransform loadingScreenTransition;

	// Use this for initialization
	void Start () {
		loadingScreenTransition = GetComponent<RectTransform> ();
		StartCoroutine (startTrue());
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(start) {
			if (loadingScreenTransition.localPosition.x > -1200) {
				loadingScreenTransition.localPosition = new Vector3 (loadingScreenTransition.localPosition.x - 40, loadingScreenTransition.localPosition.y, loadingScreenTransition.localPosition.z);
			} else {
				start = false;
			}
		}
		if(end) {
			if (loadingScreenTransition.localPosition.x < 35) {
				loadingScreenTransition.localPosition = new Vector3 (loadingScreenTransition.localPosition.x + 40, loadingScreenTransition.localPosition.y, loadingScreenTransition.localPosition.z);
			} else {
				end = false;
			}
		}
	}

	IEnumerator startTrue() {
		yield return new  WaitForSeconds(3f);
		start = true;
	}
}
