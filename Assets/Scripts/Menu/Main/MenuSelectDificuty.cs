using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelectDificuty : MonoBehaviour
{
    public string returnMenu = "MenuMain";
    public SceneFader sceneFader;
    
    public void Select(int index)
    {
        LevelController.SetDIficulty(index);
        sceneFader.FadeTo("Assets/Scenes/Menu/MenuSelector.unity");
    }
    
    public void ReturnMenu()
    {
        sceneFader.FadeTo(returnMenu);
    }
    
}
