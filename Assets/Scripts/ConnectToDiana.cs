using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ConnectToDiana : MonoBehaviour
{
    [ReadOnly]
    public bool isConnected = false; // 是否连接
    
    private JntCtrl _jntControl; // Unity中关节运动控制
    // Start is called before the first frame update
    void Start()
    {
        _jntControl = GetComponent<JntCtrl>();

        DianaApi.srv_net_st info = new DianaApi.srv_net_st();
        DianaApi.initSrvNetInfo(ref info);

        info.SrvIp = _jntControl.ipAddress;

        int ret = DianaApi.initSrv(null, null, ref info);
        
        if(ret < 0)
        {
            Debug.LogError("Diana7连接失败，IP: " + info.SrvIp);
            isConnected = false;
        }
        else
        {
            Debug.Log("Diana7连接成功，IP: " + info.SrvIp);
            isConnected = true;
        }



    }

    // Update is called once per frame
    void Update()
    {
        double [] jointPos = new double[7];
        if (isConnected)
        {
            int ret = DianaApi.getJointPos(jointPos, _jntControl.ipAddress);
            if (ret < 0)
            {
                Debug.LogError("获取关节位置失败，IP: " + _jntControl.ipAddress);
            }
            else
            {
                Debug.Log("获取关节位置成功，IP: " + _jntControl.ipAddress);
                for (int i = 0; i < 7; i++)
                {
                    _jntControl.jointAngles[i] = jointPos[i] / Mathf.PI * 180.0;
                }
            } 
        }

        
    }



    void Destroy()
    {
        DianaApi.destroySrv(_jntControl.ipAddress);
    }
}
