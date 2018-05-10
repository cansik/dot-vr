using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CircularBuffer;
using System.Linq;
using System;
using System.ComponentModel;

public class MovementTracker : MonoBehaviour
{
	public int SampleRate = 1;

	public int BufferSize = 64;

	public float ZScoreThreshold = 5.0f;

	public float MaxZScore = 0;

	public float AverageZScore = 0;

    public float JuanScore = 0;

	public bool IsMovingFast = false;
	
	private SimpleCircularBuffer<float> _buffer;

	private float _last = 0;

    // Use this for initialization
    void Start ()
	{
		_buffer = new SimpleCircularBuffer<float> (BufferSize);
		_last = this.transform.position.x;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Time.frameCount % SampleRate != 0) return;
		
		// add acceleration values
		var delta = Math.Abs(this.transform.position.x - _last);
		_buffer.Push(delta);
		_last = this.transform.position.x;
		
		// evalutate time series
		var timeSeries = _buffer.ToArray();
		var zscores = CalculateZScore (timeSeries);
		
		MaxZScore = zscores.Max();
        var maxIndex = zscores.ToList().IndexOf(MaxZScore);

        JuanScore = MaxZScore * timeSeries[maxIndex];

        IsMovingFast = MaxZScore >= ZScoreThreshold;
	}

	static IEnumerable<float> CalculateZScore(ICollection<float> data)
	{
		// precalculate mean and deviation
		var average = data.Average ();
		var sumOfSquaresOfDifferences = data.Select (val => (val - average) * (val - average)).Sum ();
		var standardDeviation = Math.Sqrt (sumOfSquaresOfDifferences / data.Count); 

		return data.Select (i => (float)((i - average) / standardDeviation)).ToArray ();
	}
}
