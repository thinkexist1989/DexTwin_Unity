using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CameraCtrl : MonoBehaviour
{
    public Transform target;

    public float distance = 5.0f; // 相机距离目标的初始距离

    public float zoomSpeed = 2.0f; // 缩放速度
    public float moveSpeed = 0.02f;

    public float minDistance = 2.0f; // 最小距离
    public float maxDistance = 15.0f; // 最大距离

    public float xSpeed = 120.0f; // 水平旋转速度
    public float ySpeed = 120.0f; // 垂直旋转速度

    public float yMinLimit = -20f; // 垂直旋转最小角
    public float yMaxLimit = 80f; // 垂直旋转最大角

    private float x = 0.0f; // 当前水平角度
    private float y = 0.0f; // 当前垂直角度

    private Vector3 _move = Vector3.zero;
    
    public MenuSwitch menuSwitch;
    public Camera configCamera;



    public float transformTime = 0.5f; // 1s
    public Transform LeftArmView;
    public Transform RightArmView;
    public Transform LeftHandView;
    public Transform RightHandView;
    private Transform initTransform; // 保存当前相机的Transform

    public bool isInPanel = false; // 是否是面板模式

    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        if (target == null)
        {
            Debug.LogWarning("CameraControl: No target assigned.");
        }
        
        initTransform = transform;
    }


    void LateUpdate()
    {
        if (!target) return;

        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if(Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            return;

        if(isInPanel)
            return;
        
        
        processInput();
        
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 限制角度范围
    private static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360f;
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }


    public void MoveToLeftArm()
    {
        isInPanel = true;
                
        transform.DOMove(LeftArmView.position, transformTime);
        transform.DORotate(LeftArmView.rotation.eulerAngles, transformTime);

    }

    public void MoveToRightArm()
    {
        isInPanel = true;

        transform.DOMove(RightArmView.position, transformTime);
        transform.DORotate(RightArmView.rotation.eulerAngles, transformTime);
    }
    
    public void MoveToLeftHand()
    {
        isInPanel = true;

        transform.DOMove(LeftHandView.position, transformTime);
        transform.DORotate(LeftHandView.rotation.eulerAngles, transformTime);

    }
    
    public void MoveToRightHand()
    {
        isInPanel = true;

        transform.DOMove(RightHandView.position, transformTime);
        transform.DORotate(RightHandView.rotation.eulerAngles, transformTime);
        
    }
    
    public void MoveToInit()
    {
        // transform.DOMove(initTransform.position, transformTime);
        // transform.DORotate(initTransform.rotation.eulerAngles, transformTime);

        isInPanel = false;

    }


    void SetXY()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void TogglePanelMode()
    {
        isInPanel = !isInPanel;
        
        Camera.main.Render();
        
        
    }

    void processInput()
    {
        if (Input.touchCount <= 0)
        {
            // === 鼠标控制 ===
            // 旋转
            if (Input.GetMouseButton(0))
            {
                menuSwitch.CloseAllMenus();
                x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            }

            // 平移
            if (Input.GetMouseButton(1))
            {
                Vector3 right = transform.right;
                Vector3 up = transform.up;

                Vector3 move = -right * (Input.GetAxis("Mouse X") * moveSpeed * distance)
                               - up * (Input.GetAxis("Mouse Y") * moveSpeed * distance);

                _move += move;
            }

            // 缩放
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.001f)
            {
                distance -= scroll * zoomSpeed;
                distance = Mathf.Clamp(distance, minDistance, maxDistance);
            }
        }
        else
        {
            // === 触控控制 ===
            if (Input.touchCount == 1)
            {
                // 单指旋转
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    x += touch.deltaPosition.x * xSpeed * 0.005f;
                    y -= touch.deltaPosition.y * ySpeed * 0.005f;
                }
            }
            else if (Input.touchCount == 2)
            {
                menuSwitch.CloseAllMenus();

                // 双指缩放
                Touch t0 = Input.GetTouch(0);
                Touch t1 = Input.GetTouch(1);

                float prevDist = (t0.position - t0.deltaPosition - (t1.position - t1.deltaPosition)).magnitude;
                float currDist = (t0.position - t1.position).magnitude;

                float delta = currDist - prevDist;
                distance -= delta * zoomSpeed * 0.005f;
                distance = Mathf.Clamp(distance, minDistance, maxDistance);

                // 处理平移
                Vector2 delta_mid = t0.deltaPosition + t1.deltaPosition;
                Vector3 right = transform.right;
                Vector3 up = transform.up;
                Vector3 move = -right * (delta_mid.x * moveSpeed * 0.5f) - up * (delta_mid.y * moveSpeed * 0.5f);

                _move += move;
            }
        }

        // === 相机变换应用 ===

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 negDistance = new Vector3(0, 0, -distance);
        Vector3 position = rotation * negDistance + target.position + _move;

        // transform.rotation = rotation;
        // transform.position = position;
        
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 5f);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }
    
}