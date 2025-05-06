using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointControl : MonoBehaviour
{
    // Diana7 Ip Address
    public string ipAddress = "192.168.1.3";
    
    public const int JointNum = 7;
    // Diana7 Joints
    public double[] jointAngles = new double[JointNum];
    
    private GameObject[] joints = new GameObject[JointNum];
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < JointNum; i++)
        {
            GameObject link = GameObject.Find("link_" + (i + 1));
            if (link)
            {
                // Set euler angles y for link
                joints[i] = link;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < JointNum; i++)
        {
            if (joints[i])
            {
                // Set euler angles y for link
                joints[i].transform.localRotation = Quaternion.Euler(joints[i].transform.localEulerAngles.x, (float)jointAngles[i], joints[i].transform.localEulerAngles.z);
            }
        }
    }
}
