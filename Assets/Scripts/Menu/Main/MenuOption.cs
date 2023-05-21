using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
    public string returnMenu = "MenuMain";
    public SceneFader sceneFader;
    
    
    
   
    public void Return()
    {
        sceneFader.FadeTo(returnMenu);
    }
    
    
}
