using Pcx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour {
    [SerializeField]
    [Range(0, 1)]
    public float InternalScore;

    [SerializeField]
    [Range(0, 1)]
    public float Score;

    public float MomentumAlpha;

    public EasingFloat momentum;

    public float MomentumSpeed;

    public float AwardSpeed;

    public float PunishmentSpeed;

    public SimpleTracker Tracker;

    public bool IsDebugging;

    [SerializeField]
    [Range(0, 1)]
    float globalExpandIndex;

    [SerializeField]
    [Range(0, 50)]
    float globalInteractionRange;

    [SerializeField]
    GameObject globalActor;

    [SerializeField]
    [Range(0.0001f, 0.05f)]
    float globalPointSize;

    [SerializeField]
    [Range(0.0001f, 0.05f)]
    float globalPointSizeStart;

    [SerializeField]
    [Range(0.0001f, 0.05f)]
    float globalPointSizeEnd;

    [SerializeField]
    Vector3 globalFloatingIntensity = Vector3.zero;

    [SerializeField]
    public Vector3 floatingIntensityStart;

    [SerializeField]
    public Vector3 floatingIntensityEnd;

    [SerializeField]
    Vector3 globalFloatingSpeed = Vector3.zero;

    [SerializeField]
    public Vector3 floatingSpeedStart;

    [SerializeField]
    public Vector3 floatingSpeedEnd;

    [SerializeField]
    public Color globalTint;

    [SerializeField]
    public Color tintStart;

    [SerializeField]
    public Color tintEnd;

    ExperienceAnimation[] exprienceAnimations;
    PointCloudRenderer[] pointClouds;

    // Use this for initialization
    void Start () {
         momentum = new EasingFloat(MomentumAlpha);

        // find all point cloud animations and renderers
        exprienceAnimations = this.GetComponentsInChildren<ExperienceAnimation>();
        pointClouds = this.GetComponentsInChildren<PointCloudRenderer>();

        foreach(var ani in exprienceAnimations)
        {
            ani.actor = globalActor;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!IsDebugging)
        {
            CaluclateScore();
        }

        ApplyScore();

        UpdateExperienceAnimations();
        UpdatePointClouds();
    }

    void ApplyScore()
    {
        // change global score
        Score = 1.0f - InternalScore;

        globalExpandIndex = Score;
        globalPointSize = Mathf.Lerp(globalPointSizeStart, globalPointSizeEnd, Score);

        globalTint = Color.Lerp(tintStart, tintEnd, Score);

        LerpFloatingValues();
    }

    void CaluclateScore()
    {
        if (Tracker.SpeedIsGood)
        {
            momentum.Target += MomentumSpeed;
        }
        else
        {
            momentum.Target -= MomentumSpeed;
        }

        momentum.Target = Mathf.Clamp(momentum.Target, PunishmentSpeed, AwardSpeed);
        momentum.Update();

        InternalScore += momentum.Value;
        InternalScore = Mathf.Clamp(InternalScore, 0, 1);
    }

    void LerpFloatingValues()
    {
        globalFloatingSpeed = Vector3.Lerp(floatingSpeedStart, floatingSpeedEnd, Score);
        globalFloatingIntensity = Vector3.Lerp(floatingIntensityStart, floatingIntensityEnd, Score);
    }

    void UpdateExperienceAnimations()
    {
        foreach(var expAni in exprienceAnimations)
        {
            expAni.ExpandIndex = globalExpandIndex;
            expAni.FloatingIntensity = globalFloatingIntensity;
            expAni.FloatingSpeed = globalFloatingSpeed;
            expAni.minDistance = globalInteractionRange;
        }
    }

    void UpdatePointClouds()
    {
        foreach (var clouds in pointClouds)
        {
            clouds.pointSize = globalPointSize;
            clouds.pointTint = globalTint;
        }
    }
}
