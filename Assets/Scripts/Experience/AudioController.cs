using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {

    public ExperienceController experience;

    public AudioMixer jungleMixer;

    public AudioMixer spaceMixer;

    public float maxVolume = 0;

    public float minVolume = -80;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // update audio volume
        jungleMixer.SetFloat("Volume", Mathf.Lerp(minVolume, maxVolume, experience.Score));
        spaceMixer.SetFloat("Volume", Mathf.Lerp(maxVolume, minVolume, experience.Score));
    }
}
