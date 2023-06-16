using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;

public class MainMenu : MonoBehaviourPunCallbacks
{
    [Header("Panels")]
    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject difficultyPanel;

    [SerializeField]
    private GameObject gameModePanel;

    [SerializeField]
    private GameObject easyPanel;

    [SerializeField]
    private GameObject mediumPanel;

    [SerializeField]
    private GameObject hardPanel;

    [SerializeField]
    private GameObject connectionPanel;

    [SerializeField] 
    private InputField loginInput;

    [SerializeField]
    private GameObject createJoinPanel;

    [SerializeField]
    private GameObject multiGameMode;

    [SerializeField]
    private GameObject roomPanel;

    [SerializeField]
    private GameObject listRoomPanel;

    [SerializeField]
    private Transform listRoomContent;

    [SerializeField]
    private GameObject roomButton;

    [Header("Level Selector")]
    [SerializeField]
    private Transform easyContent;

    [SerializeField] 
    private Transform mediumContent;

    [SerializeField] 
    private Transform hardContent;

    [SerializeField]
    private GameObject buttonPrefab;

    private GameObject actualPanel;

    private string nickname;

    private List<string> easyLevels = new List<string>();

    private List<string> mediumLevels = new List<string>();

    private List<string> hardLevels = new List<string>();

    private List<RoomInfo> roomInfos = new List<RoomInfo>();

    private void Start()
    {
        FindLevels();
        PlaceButton();
    }

    public void Connect()
    {
        if (loginInput.text.Length > 3 && !loginInput.text.Contains(' '))
        {
            Debug.Log("Connection au serveur...");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connecté au serveur !");
        Debug.Log("Connection au lobby...");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby rejoint !");
        nickname = loginInput.text;
        actualPanel = createJoinPanel;
        connectionPanel.SetActive(false);
        createJoinPanel.SetActive(true);
    }

    public void Disconnect()
    {
        Debug.Log("Déconnection du serveur...");
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Déconnecté du serveur !");
        actualPanel.SetActive(false);
        actualPanel = connectionPanel;
        connectionPanel.SetActive(true);
    }

    public void CreateRoom(string gameMode)
    {
        Debug.Log("Création de la room...");
        Hashtable customProperties = new Hashtable();
        if (gameMode == "Green")
        {
            customProperties.Add("GameMode", "Green");
        }
        else if (gameMode == "Green Circle")
        {
            customProperties.Add("GameMode", "Green Circle");
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.CustomRoomProperties = customProperties;
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "GameMode" };
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 9;
        PhotonNetwork.CreateRoom(nickname, roomOptions);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("La room a été créée !");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connecté à la room -> " + PhotonNetwork.CurrentRoom.Name);
        actualPanel.SetActive(false);
        actualPanel = roomPanel;
        roomPanel.SetActive(true);
    }

    public void LeaveRoom()
    {
        Debug.Log("Déconnection de la room...");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("La room a été quittée !");
        actualPanel = createJoinPanel;
        roomPanel.SetActive(false);
        createJoinPanel.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        roomInfos = roomList;
        PlaceRoomButton();
    }

    public void ListRoom()
    {
        actualPanel = listRoomPanel;
        createJoinPanel.SetActive(false);
        listRoomPanel.SetActive(true);
    }

    public void Create()
    {
        actualPanel = multiGameMode;
        createJoinPanel.SetActive(false);
        multiGameMode.SetActive(true);
    }

    public void Back(GameObject panel)
    {
        actualPanel.SetActive(false);
        panel.SetActive(true);
        actualPanel = panel;
    }

    public void Play()
    {
        actualPanel = gameModePanel;
        mainPanel.SetActive(false); 
        gameModePanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Solo()
    {
        actualPanel = difficultyPanel;
        gameModePanel.SetActive(false);
        difficultyPanel.SetActive(true);
    }

    public void Multiplayer()
    {
        actualPanel = connectionPanel;
        gameModePanel.SetActive(false);
        connectionPanel.SetActive(true);
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

    private void PlaceRoomButton()
    {
        foreach (RoomInfo roomInfo in roomInfos)
        {
            GameObject button = Instantiate(roomButton, listRoomContent);
            button.GetComponentInChildren<Text>().text = roomInfo.CustomProperties["GameMode"] + " : " + roomInfo.Name;
            button.GetComponent<RoomButton>().roomName = roomInfo.Name;
        }
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
