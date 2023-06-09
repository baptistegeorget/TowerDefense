using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;
    
    public void ReturnMenu()
    {
        sceneFader.FadeTo("Assets/Scenes/Menu/MenuSelector.unity");
    }

    public void Retry()
    {
        sceneFader.FadeTo(LevelController.GetLevel());
    }
}
