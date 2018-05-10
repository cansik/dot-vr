using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingFloat {

    public float Alpha { get; set; }

    public float Target { get; set; }
    public float Value { get; set; }

    public EasingFloat(float alpha)
    {
        Alpha = alpha;
    }

    public void Update()
    {
        Value += (Target - Value) * Alpha;
    }
}
