using CircularBuffer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTracker : MonoBehaviour {

    public float PositionAlpha;

    public float RotationAlpha;

    public float PositionMaxThreshold;

    public float PositionMinThreshold;

    public float RotationMaxThreshold;

    public float RotationMinThreshold;

    public bool SpeedIsGood;

    public bool IsTooFast;

    public bool IsTooSlow;

    public bool PositionIsTooFast;

    public bool RotationIsTooFast;

    public bool PositionIsTooSlow;

    public bool RotationIsTooSlow;

    public Vector3 AveragePosition;

    public Vector3 AverageRotation;

    private VectorEMAverage _averagePosition;

    private VectorEMAverage _averageRotation;

    private Vector3 _lastPosition = Vector3.zero;

    private Vector3 _lastRotation = Vector3.zero;

    // Use this for initialization
    void Start () {
        _averagePosition = new VectorEMAverage(PositionAlpha);
        _averageRotation = new VectorEMAverage(RotationAlpha);
    }
	
	// Update is called once per frame
	void Update () {
        var eulerAngles = this.transform.rotation.ToEulerAngles();

        var positionDelta = AbsVector(transform.position - _lastPosition);
        var rotationDelta = AbsVector(eulerAngles - _lastRotation);

        _lastPosition = this.transform.position;
        _lastRotation = eulerAngles;

        _averagePosition.Add(positionDelta);
        _averageRotation.Add(rotationDelta);

        AveragePosition = _averagePosition.AverageVector;
        AverageRotation = _averageRotation.AverageVector;

        // check thresholds
        PositionIsTooFast = IsOverThreshold(_averagePosition.AverageVector, PositionMaxThreshold);
        RotationIsTooFast = IsOverThreshold(_averageRotation.AverageVector, RotationMaxThreshold);

        IsTooFast = PositionIsTooFast || RotationIsTooFast;

        PositionIsTooSlow = !IsOverThreshold(_averagePosition.AverageVector, PositionMinThreshold);
        RotationIsTooSlow = !IsOverThreshold(_averageRotation.AverageVector, RotationMinThreshold);

        IsTooSlow = PositionIsTooSlow || RotationIsTooSlow;

        SpeedIsGood = !IsTooSlow && !IsTooFast;
    }

    static bool IsOverThreshold(Vector3 vector, float threshold)
    {
        return (vector.x >= threshold
            || vector.y >= threshold
            || vector.z >= threshold);
    }

    static Vector3 AbsVector(Vector3 vector)
    {
        return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.z), Mathf.Abs(vector.y));
    }
}
