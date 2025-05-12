using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitch : MonoBehaviour
{

    public SlideMenu menuTasks;
    public SlideMenu menuStates;
    public SlideMenu menuSettings;
    
    
    public void ToggleTaskMenu()
    {
        menuTasks.ToggleMenu();
        menuStates.isOpen = false;
        menuSettings.isOpen = false;
    }
    
    public void ToggleStateMenu()
    {
        menuTasks.isOpen = false;
        menuStates.ToggleMenu();
        menuSettings.isOpen = false;
    }
    
    public void ToggleSettingMenu()
    {
        menuTasks.isOpen = false;
        menuStates.isOpen = false;
        menuSettings.ToggleMenu();
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
