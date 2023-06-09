using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    
    public SceneFader sceneFader;
    
    public void NextLevel()
    {
        sceneFader.FadeTo(LevelController.NextLevel());
    }
    public void ReturnMenu()
    {
        sceneFader.FadeTo("Assets/Scenes/Menu/MenuSelector.unity");
    }

    public void Retry()
    {
        sceneFader.FadeTo(LevelController.GetLevel());
    }
}
