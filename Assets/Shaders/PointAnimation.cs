using UnityEngine;
using Pcx;

[ExecuteInEditMode]
public class PointAnimation : MonoBehaviour
{
    [SerializeField] PointCloudData _sourceData;
    [SerializeField] ComputeShader _computeShader;

    [SerializeField] Color color;
    [SerializeField] Color activeColor;

    [SerializeField] GameObject actor;

    [SerializeField] float minDistance;


    ComputeBuffer _pointBuffer;

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
        _computeShader.SetVector("Color", color);
        _computeShader.SetVector("ActiveColor", activeColor);
        _computeShader.SetVector("Actor", actor.transform.position);
        _computeShader.SetFloat("Time", time);
        _computeShader.SetFloat("MinDistance", minDistance);
        _computeShader.SetBuffer(kernel, "SourceBuffer", sourceBuffer);
        _computeShader.SetBuffer(kernel, "OutputBuffer", _pointBuffer);
        _computeShader.Dispatch(kernel, sourceBuffer.count / 128, 1, 1);

        GetComponent<PointCloudRenderer>().sourceBuffer = _pointBuffer;
    }
}
