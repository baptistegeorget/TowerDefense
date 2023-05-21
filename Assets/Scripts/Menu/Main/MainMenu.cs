using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string play = "MenuGameSelector";
    public string option = "MenuOption";
    public string returnMenu = "MenuMain";

    public SceneFader sceneFader;


    public void Play()
    {
        sceneFader.FadeTo(play);
    }

    public void Option()
    {
        sceneFader.FadeTo(option);
    }

    public void Return()
    {
        sceneFader.FadeTo(returnMenu);
    }

    public void Quit()
    {
        Debug.Log("Le jeu est quit�");
        // ne fonctionera pas sur l'�diteur
        Application.Quit();
    }
}
