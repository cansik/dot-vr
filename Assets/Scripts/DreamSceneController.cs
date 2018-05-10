using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamSceneController : MonoBehaviour {

    public PointAnimation pointAnimation;

    bool isPressDown = false;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Trigger))
            isPressDown = !isPressDown;

        pointAnimation.reColorCloud = isPressDown;
    }
}
