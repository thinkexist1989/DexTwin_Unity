using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitch : MonoBehaviour
{

    public SlideMenu menuTasks;
    public SlideMenu menuArms;
    public SlideMenu menuHands;
    public SlideMenu menuSettings;
    public SlideMenu menuAbout;
    
    
    public void ToggleTaskMenu()
    {
        menuTasks.ToggleMenu();
        menuArms.isOpen = false;
        menuHands.isOpen = false;
        menuSettings.isOpen = false;
        menuAbout.isOpen = false;
    }
    
    public void ToggleArmsMenu()
    {
        menuTasks.isOpen = false;
        menuArms.ToggleMenu();
        menuHands.isOpen = false;
        menuSettings.isOpen = false;
        menuAbout.isOpen = false;

    }
    
    public void ToggleHandsMenu()
    {
        menuTasks.isOpen = false;
        menuArms.isOpen = false;
        menuHands.ToggleMenu();
        menuSettings.isOpen = false;
        menuAbout.isOpen = false;

    }

    public void ToggleSettingMenu()
    {
        menuTasks.isOpen = false;
        menuArms.isOpen = false;
        menuHands.isOpen = false;
        menuSettings.ToggleMenu();
        menuAbout.isOpen = false;
    }
    
    public void ToggleAboutMenu()
    {
        menuTasks.isOpen = false;
        menuArms.isOpen = false;
        menuHands.isOpen = false;
        menuSettings.isOpen = false;
        menuAbout.ToggleMenu();
    }
    
    
    public void CloseAllMenus()
    {
        menuTasks.isOpen = false;
        menuArms.isOpen = false;
        menuHands.isOpen = false;
        menuSettings.isOpen = false;
        menuAbout.isOpen = false;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
