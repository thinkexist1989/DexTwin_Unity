using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFingerJntCtrl : MonoBehaviour
{


    public float value;
    
    public float minValue = 0.0f;
    public float maxValue = 90.0f;

    public enum DirectionType
    {
        Positive = 1,
        Negative = -1
    }
    
    public DirectionType direction = DirectionType.Positive;
    
    private Quaternion initRot;
    
    // Start is called before the first frame update
    void Start()
    {
        initRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        value = Mathf.Clamp(value,minValue,maxValue);
        transform.localRotation = initRot * Quaternion.Euler(0, (float)(direction == DirectionType.Positive ? value : -value), 0);
    }
}
