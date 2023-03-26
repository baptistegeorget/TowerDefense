using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string LevelToload = "MenuSelector";

    public SceneFader sceneFader;


    public void Play()
    {
        sceneFader.FadeTo(LevelToload);
    }

    public void Option()
    {
        Debug.Log("Menu Option");
    }

    public void Quit()
    {
        Debug.Log("Le jeu est quit�");
        // ne fonctionera pas sur l'�diteur
        Application.Quit();
    }
}
