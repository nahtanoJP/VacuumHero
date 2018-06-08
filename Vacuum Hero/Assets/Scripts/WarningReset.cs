using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningReset : MonoBehaviour {

	float time = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<Text>().text != "") {
			time += Time.deltaTime;
			if(time >= 5f) {
				time = 0;
				gameObject.GetComponent<Text> ().text = "";
			}
		}
	}
}
