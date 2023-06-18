using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene(name);
    }
}
