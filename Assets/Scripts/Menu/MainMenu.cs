using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad = "MenuSelector";

    public SceneFader sceneFader;

    public void Play()
    {
        sceneFader.FadeTo(LevelToLoad);
    }

    public void Option()
    {
        Debug.Log("Menu Option");
    }

    public void Quit()
    {
        Debug.Log("Le jeu est quité");
        Application.Quit();
    }
}
