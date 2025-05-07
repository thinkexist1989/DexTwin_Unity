using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject target;
    
    public float distance = 5.0f;      // 相机距离目标的初始距离
    public float zoomSpeed = 2.0f;     // 缩放速度
    public float minDistance = 2.0f;   // 最小距离
    public float maxDistance = 15.0f;  // 最大距离

    public float xSpeed = 120.0f;      // 水平旋转速度
    public float ySpeed = 120.0f;      // 垂直旋转速度

    public float yMinLimit = -20f;     // 垂直旋转最小角
    public float yMaxLimit = 80f;      // 垂直旋转最大角

    private float x = 0.0f;            // 当前水平角度
    private float y = 0.0f;            // 当前垂直角度


    public GameObject orientationCamera;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

    }


    void LateUpdate()
    {
        if (target)
        {
            // 鼠标右键控制旋转
            if (Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }

            // 鼠标滚轮控制缩放
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            distance -= scroll * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 direction = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * direction + target.transform.position;

            transform.rotation = rotation;
            transform.position = position;

            if (orientationCamera)
            {
                orientationCamera.transform.localRotation = rotation;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // 限制角度范围
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
