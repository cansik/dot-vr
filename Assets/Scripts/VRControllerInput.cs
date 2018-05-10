using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRControllerInput : MonoBehaviour {

    public GameObject controller;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = controller.transform.rotation;
        this.transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y, controller.transform.position.z);
    }
}
