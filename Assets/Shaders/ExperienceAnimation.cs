using Pcx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceAnimation : MonoBehaviour {

    [SerializeField]
    [Range(0, 1)]
    private float expandIndex;

    [SerializeField]
    public Vector3 FloatingIntensity { get; set; }

    [SerializeField]
    public Vector3 FloatingSpeed { get; set; }

    [SerializeField]
    public GameObject actor;

    [SerializeField]
    public float minDistance;

    [SerializeField] PointCloudData _sourceData;
    [SerializeField] ComputeShader _computeShader;

    ComputeBuffer _pointBuffer;

    public float ExpandIndex
    {
        get
        {
            return expandIndex;
        }
        set
        {
            expandIndex = value;
        }
    }

void OnDisable()
    {
        if (_pointBuffer != null)
        {
            _pointBuffer.Release();
            _pointBuffer = null;
        }
    }

    void Update()
    {
        if (_sourceData == null) return;

        var sourceBuffer = _sourceData.computeBuffer;

        if (_pointBuffer == null || _pointBuffer.count != sourceBuffer.count)
        {
            if (_pointBuffer != null) _pointBuffer.Release();
            _pointBuffer = new ComputeBuffer(sourceBuffer.count, PointCloudData.elementSize);
        }

        var time = Application.isPlaying ? Time.time : 0;
        var kernel = _computeShader.FindKernel("Main");

        // calculate the position
        var localPos = this.transform.InverseTransformVector(actor.transform.position - this.transform.position);

        _computeShader.SetFloat("time", time);

        _computeShader.SetFloat("expandIndex", ExpandIndex);

        _computeShader.SetVector("floatingSpeed", FloatingSpeed);
        _computeShader.SetVector("floatingIntensity", FloatingIntensity);

        _computeShader.SetVector("actor", localPos);
        _computeShader.SetFloat("minDistance", minDistance);

        _computeShader.SetBuffer(kernel, "SourceBuffer", sourceBuffer);
        _computeShader.SetBuffer(kernel, "OutputBuffer", _pointBuffer);
        _computeShader.Dispatch(kernel, sourceBuffer.count / 128, 1, 1);

        GetComponent<PointCloudRenderer>().sourceBuffer = _pointBuffer;
    }
}
