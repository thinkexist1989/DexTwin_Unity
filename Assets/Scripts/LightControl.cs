using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Material redLight;
    public Material greenLight;
    public Material blueLight;
    public Material whiteLight;

    public int lightIndex = 3;
    public GameObject body;
    
    
    private MeshRenderer bodyRenderer;
    
    
    public enum LightColor
    {
        Red,
        Green,
        Blue,
        White
    }
    
    public LightColor lightColor = LightColor.Green;
    
    // Start is called before the first frame update
    void Start()
    {
        if(body)
            bodyRenderer = body.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Material[] mats = bodyRenderer.materials;
        
        if (lightIndex < 0 || lightIndex >= mats.Length)
        {
            Debug.LogWarning("材质索引超出范围！");
            return;
        }
        
        switch (lightColor)
        {
            case LightColor.Blue:
                mats[lightIndex] = blueLight;
                break;
            case LightColor.Green:
                mats[lightIndex] = greenLight;
                break;
            case LightColor.Red:
                mats[lightIndex] = redLight;
                break;
            case LightColor.White:
                mats[lightIndex] = whiteLight;
                break;
            default:
                mats[lightIndex] = whiteLight;
                break;
        }
        
        bodyRenderer.materials = mats;
    }

    void LateUpdate()
    {

    }
}
