using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PFXController : MonoBehaviour {

    public ExperienceController experience;

    public SimpleTracker tracker;

    public PostProcessVolume volume;

    Bloom bloomLayer = null;
    ChromaticAberration chromaticLayer = null;

    // Use this for initialization
    void Start () {
        volume.profile.TryGetSettings(out bloomLayer);
        volume.profile.TryGetSettings(out chromaticLayer);
    }
	
	// Update is called once per frame
	void Update () {
        // control bloom
        bloomLayer.intensity.value = Mathf.Lerp(4.5f, 0.0f, experience.Score);

        // set chromatic
        //Debug.Log(experience.momentum.Value);
        //chromaticLayer.intensity.value = Mathf.Lerp(0, 1, (tracker.IsTooFast || tracker.IsTooSlow) ? 1 : 0);
    }
}
