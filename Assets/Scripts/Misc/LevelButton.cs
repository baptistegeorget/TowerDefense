using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public void StartLevel()
    {
        SceneManager.LoadScene(name);
    }
}
