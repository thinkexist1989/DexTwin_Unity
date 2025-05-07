using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CoordinateSystem : MonoBehaviour
{
    
    public float axisLength = 0.5f; // 坐标轴长度
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos, pos + Vector3.right * axisLength);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos, pos + Vector3.up * axisLength);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos, pos + Vector3.forward * axisLength);
    }
    
}
