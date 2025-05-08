using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JntCtrl : MonoBehaviour
{
    // Diana7 Ip Address
    public string ipAddress = "192.168.1.3";

    public GameObject link_1;
    public GameObject link_2;
    public GameObject link_3;
    public GameObject link_4;
    public GameObject link_5;
    public GameObject link_6;
    public GameObject link_7;
    
    
    
    public const int JointNum = 7;
    // Diana7 Joints
    public double[] jointAngles = new double[JointNum];
    
    private GameObject[] links = new GameObject[JointNum];
    
    private Quaternion[] initRot = new Quaternion[JointNum];
    
    private int[] jointDir = new int[JointNum];

    // Start is called before the first frame update
    void Start()
    {
        links[0] = link_1;
        links[1] = link_2;
        links[2] = link_3;
        links[3] = link_4;
        links[4] = link_5;
        links[5] = link_6;
        links[6] = link_7;

        for (int i = 0; i < JointNum; i++)
        {
            if (links[i])
            {
                initRot[i] = links[i].transform.localRotation;
            }
        }

        jointDir[0] = 1;
        jointDir[1] = -1;
        jointDir[2] = 1;
        jointDir[3] = -1;
        jointDir[4] = 1;
        jointDir[5] = 1;
        jointDir[6] = -1;
        
        // for (int i = 0; i < JointNum; i++)
        // {
        //     // Transform link = transform.Find("link_" + (i + 1));
        //     if (link)
        //     {
        //         Debug.Log("Found link: " + link.name);
        //         // Set euler angles y for link
        //         joints[i] = link;
        //         jointInitRot[i] = link.transform.localRotation;
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < JointNum; i++)
        {
            if (links[i])
            {
                // Set euler angles y for link
                links[i].transform.localRotation =
                    initRot[i] * Quaternion.Euler(0, (float)jointAngles[i] * jointDir[i], 0);
                // joints[i].transform.localRotation = Quaternion.Euler(joints[i].transform.localEulerAngles.x, (float)jointAngles[i], joints[i].transform.localEulerAngles.z);

            }
        }
    }
}
