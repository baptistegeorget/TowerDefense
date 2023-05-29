using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public string retry = "MenuGameSelector";
    public string returnMenu = "MenuMain";
    public SceneFader sceneFader;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
