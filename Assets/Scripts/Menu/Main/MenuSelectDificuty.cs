using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelectDificuty : MonoBehaviour
{
    public string debutant;
    public string intermediaire;
    public string expert;
    public string returnMenu = "MenuMain";
    public SceneFader sceneFader;
    
    public void Debutant()
    {
        sceneFader.FadeTo(debutant);
    }
    public void Intermediaire()
    {
        sceneFader.FadeTo(intermediaire);
    }
    public void Expert()
    {
        sceneFader.FadeTo(expert); 
    }
    
}
