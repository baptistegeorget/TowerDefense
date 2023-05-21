using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGameSelector : MonoBehaviour
{
    public string returnMenu = "MenuMain";
    public SceneFader sceneFader;
    public string solo = "MenuSelector";
    
    public void Solo()
    {
        sceneFader.FadeTo(solo);
    }
    public void Return()
    {
        sceneFader.FadeTo(returnMenu);
    }
}
