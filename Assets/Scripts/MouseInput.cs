using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3((Input.mousePosition.normalized.x - 0.5f) * 5f, this.transform.position.y, this.transform.position.z);
    }
}
