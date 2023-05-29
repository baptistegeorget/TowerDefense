using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public string retry = "MenuGameSelector";
    public string returnMenu = "MenuMain";
    public string nextLevel = "MenuGameSelector";
    public SceneFader sceneFader;
    
    public void NextLevel()
    {
        sceneFader.FadeTo(nextLevel);
    }
    public void ReturnMenu()
    {
        sceneFader.FadeTo(returnMenu);
    }

    public void Retry()
    {
        sceneFader.FadeTo(retry);
    }
}
