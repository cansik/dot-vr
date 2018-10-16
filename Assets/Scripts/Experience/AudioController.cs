using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {

    public ExperienceController experience;

    public AudioMixer jungleMixer;

    public AudioMixer spaceMixer;

    public float maxVolume = 0;

    public float minVolume = -40;

    public AnimationCurve jungleCurve;

    public AnimationCurve spaceCurve;

    public AnimationCurve spaceHighPassCutoffCurve;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // update audio volume
        jungleMixer.SetFloat("Pitch", Mathf.Lerp(0.10f, 1, experience.Score));
        jungleMixer.SetFloat("Volume", Mathf.Lerp(minVolume, maxVolume, jungleCurve.Evaluate(experience.Score)));

        spaceMixer.SetFloat("Volume", Mathf.Lerp(maxVolume, minVolume, spaceCurve.Evaluate(experience.Score)));
        spaceMixer.SetFloat("HPassCut", Mathf.Lerp(10, 1500, spaceHighPassCutoffCurve.Evaluate(experience.Score)));
    }
}
