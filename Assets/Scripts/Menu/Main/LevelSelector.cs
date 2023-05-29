using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;
    public string dificulty = "levelReachedDebutant";
    public string antecedant = "MenuSelectDificuty";
    

    public SceneFader sceneFader;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt(dificulty, 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void Select(string levelName)
    {
        //sceneFader.FadeTo(levelName);
        SceneManager.LoadScene(0);
        //SceneManager.LoadScene(levelName);
    }

    public void ReturnMenu()
    {
        sceneFader.FadeTo(antecedant);
    }
}