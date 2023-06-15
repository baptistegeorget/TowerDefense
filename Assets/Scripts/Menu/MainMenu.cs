using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject difficultyPanel;

    [SerializeField]
    private GameObject gamemodePanel;

    [SerializeField]
    private GameObject easyPanel;

    [SerializeField]
    private GameObject mediumPanel;

    [SerializeField]
    private GameObject hardPanel;

    [SerializeField]
    private GameObject multiplayerPanel;

    [Header("Level Selecter")]
    [SerializeField]
    private Transform easyContent;

    [SerializeField] 
    private Transform mediumContent;

    [SerializeField] 
    private Transform hardContent;

    [SerializeField]
    private GameObject buttonPrefab;

    private GameObject actualPanel;

    private List<string> easyLevels = new List<string>();

    private List<string> mediumLevels = new List<string>();

    private List<string> hardLevels = new List<string>();

    private void Start()
    {
        FindLevels();
        PlaceButton();
    }

    public void Back(GameObject panel)
    {
        actualPanel.SetActive(false);
        panel.SetActive(true);
        actualPanel = panel;
    }

    public void Play()
    {
        actualPanel = gamemodePanel;
        mainPanel.SetActive(false); 
        gamemodePanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Solo()
    {
        actualPanel = difficultyPanel;
        gamemodePanel.SetActive(false);
        difficultyPanel.SetActive(true);
    }

    public void Multiplayer()
    {
        actualPanel = multiplayerPanel;
        gamemodePanel.SetActive(false);
        multiplayerPanel.SetActive(true);
    }

    public void Easy()
    {
        actualPanel = easyPanel;
        difficultyPanel.SetActive(false);
        easyPanel.SetActive(true);
    }

    public void Medium()
    {
        actualPanel = mediumPanel;
        difficultyPanel.SetActive(false);
        mediumPanel.SetActive(true);
    }

    public void Hard()
    {
        actualPanel = hardPanel;
        difficultyPanel.SetActive(false);
        hardPanel.SetActive(true);
    }

    private void PlaceButton()
    {
        for (int i = 0; i < easyLevels.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab, easyContent);
            button.GetComponentInChildren<Text>().text = (i + 1).ToString();
            button.name = easyLevels[i].ToString();
        }
        for (int i = 0; i < mediumLevels.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab, mediumContent);
            button.GetComponentInChildren<Text>().text = (i + 1).ToString();
            button.name = mediumLevels[i].ToString();
        }
        for (int i = 0; i < hardLevels.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab, hardContent);
            button.GetComponentInChildren<Text>().text = (i + 1).ToString();
            button.name = hardLevels[i].ToString();
        }
    }

    private void FindLevels()
    {
        List<string> sceneNames = new List<string>();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            sceneNames.Add(sceneName);
        }
        foreach (string sceneName in sceneNames)
        {
            if (sceneName.Split(' ')[0] == "Level")
            {
                string[] sceneInfos = sceneName.Split(" ")[1].Split('.');
                if (sceneInfos[0] == "1")
                {
                    easyLevels.Add(sceneName);
                }
                else if (sceneInfos[0] == "2")
                {
                    mediumLevels.Add(sceneName);
                }
                else if (sceneInfos[0] == "3")
                {
                    hardLevels.Add(sceneName);
                }
            }
        }
    }
}
