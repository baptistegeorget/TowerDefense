using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelSelector : MonoBehaviour
{
    public Button buttonPrefab;
    public Transform ButtonContainer;
    public Sprite[] buttonImageActive;
    public Sprite[] buttonImageDisable;
    public string antecedant = "MenuSelectDificuty";
    private string[] levels;

    public SceneFader sceneFader;

    private void Start()
    {
        levels = LevelController.GetLevels();

        decimal colorSwitch = Math.Round((decimal)(levels.Length / 3));
        for (int i = 0; i < levels.Length; i++)
        {
            int levelIndex = i;
            
            Button button = Instantiate(buttonPrefab, ButtonContainer, false);
            button.onClick.AddListener(() => Select(levelIndex)); 
            button.GetComponentInChildren<Text>().text = (i + 1).ToString();
            
            if (i > colorSwitch)
            {
                if (colorSwitch == i)
                {
                    colorSwitch += Math.Round((decimal)(levels.Length / 3));
                }
            } 

            button.GetComponent<Image>().sprite = buttonImageActive[0];
            if (i > LevelController.level)
            {
                button.interactable = false;
                button.GetComponent<Image>().sprite = buttonImageDisable[0];
                button.GetComponentInChildren<Text>().color = Color.gray;
            }
        }
    
    }

    public void Select(int indexLevel)
    {
        LevelController.CurenteLevel = indexLevel;
        sceneFader.FadeTo(levels[indexLevel]);
    }

    public void ReturnMenu()
    {
        sceneFader.FadeTo(antecedant);
    }
}