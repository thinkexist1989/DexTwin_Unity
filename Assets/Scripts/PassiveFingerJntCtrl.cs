using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PassiveFingerJntCtrl : MonoBehaviour
{
    public ActiveFingerJntCtrl activeFinger;
    
    public enum DirectionType
    {
        Positive = 1,
        Negative = -1
    }
    
    public DirectionType direction = DirectionType.Positive;
    
    public double multiplier = 1.0;
    public double offset = 0.0;
    
    public double value = 0.0;
    
    private Quaternion initRot;
    
    // Start is called before the first frame update
    void Start()
    {
        initRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        value = activeFinger.value * multiplier;
        transform.localRotation = initRot * Quaternion.Euler(0, (float)(direction == DirectionType.Positive ? value : -value), 0);
    }
}
