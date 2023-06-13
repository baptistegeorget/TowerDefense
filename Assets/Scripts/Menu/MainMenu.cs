using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Assets/Scenes/Menu/MenuSelector.unity");
    }

    public void Option()
    {
        Debug.Log("Option");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
