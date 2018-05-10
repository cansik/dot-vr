using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var normalizedMousePosition = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, Input.mousePosition.z);
        this.transform.position = new Vector3((normalizedMousePosition.x - 0.5f) * 8f, (normalizedMousePosition.y - 0.5f) * 8f, this.transform.position.z);
    }
}