using CircularBuffer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorEMAverage {
    private ExponentialMovingAverage _averageX;
    private ExponentialMovingAverage _averageY;
    private ExponentialMovingAverage _averageZ;


    public VectorEMAverage(float alpha)
    {
        _averageX = new ExponentialMovingAverage(alpha);
        _averageY = new ExponentialMovingAverage(alpha);
        _averageZ = new ExponentialMovingAverage(alpha);
    }

    public void Add(Vector3 vector)
    {
        this._averageX.Add(vector.x);
        this._averageY.Add(vector.y);
        this._averageZ.Add(vector.z);
    }

    public Vector3 AverageVector { get
        {
            return new Vector3((float)_averageX.Average, (float)_averageY.Average, (float)_averageZ.Average);
        }
    }
}
