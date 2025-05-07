using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConnectToDiana : MonoBehaviour
{
    
    private JointControl jointControl; // Unity中关节运动控制
    // Start is called before the first frame update
    void Start()
    {
        jointControl = GetComponent<JointControl>();

        DianaApi.srv_net_st info = new DianaApi.srv_net_st();
        DianaApi.initSrvNetInfo(ref info);

        info.SrvIp = jointControl.ipAddress;

        int ret = DianaApi.initSrv(null, null, ref info);
        
        if(ret < 0)
        {
            Debug.LogError("Diana7连接失败，IP: " + info.SrvIp);
        }
        else
        {
            Debug.Log("Diana7连接成功，IP: " + info.SrvIp);
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void Destroy()
    {
        DianaApi.destroySrv(jointControl.ipAddress);
    }
}
