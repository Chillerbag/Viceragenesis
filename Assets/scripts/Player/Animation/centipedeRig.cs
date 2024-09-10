using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class centipedeRig : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float dampWeight;

    private void Awake()
    {
        foreach (DampedTransform dampedTransform in GetComponentsInChildren<DampedTransform>())
        {
            dampedTransform.data.dampPosition = dampWeight;
            dampedTransform.data.dampRotation = dampWeight;
            //dampedTransform.weight = dampWeight;
        }
    }

}
