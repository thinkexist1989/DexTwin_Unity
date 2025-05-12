using UnityEngine;

public class SlideMenu : MonoBehaviour
{
    public RectTransform menuPanel;  // 菜单面板的 RectTransform
    public float slideSpeed = 5f;    // 滑动速度
    
    // public float hidePosition = -900f; // 菜单隐藏时的位置
    public float showPosition = 130f;

    public bool isOpen = false;

    void Update()
    {
        // 按下按钮时打开或关闭菜单
        if (Input.GetKeyDown(KeyCode.Space))  // 在这里你可以替换成按钮点击事件
        {
            ToggleMenu();
        }

        // 控制菜单的滑动动画
        float targetPosX = isOpen ? showPosition : -menuPanel.rect.width;
        // float targetPosX = isOpen ? showPosition : hidePosition;
        menuPanel.anchoredPosition = Vector2.Lerp(menuPanel.anchoredPosition, new Vector2(targetPosX, 0), Time.deltaTime * slideSpeed);
    }

    public void ToggleMenu()
    {
        isOpen = !isOpen;  // 切换菜单打开和关闭状态
    }
}