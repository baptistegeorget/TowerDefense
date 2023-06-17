using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using System;

public class MainMenu : MonoBehaviourPunCallbacks
{
    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;

    [SerializeField] private GameObject difficultyPanel;

    [SerializeField] private GameObject gameModePanel;

    [SerializeField] private GameObject easyPanel;

    [SerializeField] private GameObject mediumPanel;

    [SerializeField] private GameObject hardPanel;

    [SerializeField] private GameObject connectionPanel;

    [SerializeField] private InputField loginInput;

    [SerializeField] private GameObject createJoinPanel;

    [SerializeField] private GameObject multiGameMode;

    [SerializeField] private GameObject roomPanel;

    [SerializeField] private Transform roomContent;

    [SerializeField] private GameObject playerButton;

    [SerializeField] private GameObject listRoomPanel;

    [SerializeField] private Transform listRoomContent;

    [SerializeField] private GameObject roomButton;

    [Header("Level Selector")]
    [SerializeField] private Transform easyContent;

    [SerializeField] private Transform mediumContent;

    [SerializeField] private Transform hardContent;

    [SerializeField] private GameObject buttonPrefab;

    private GameObject actualPanel;

    private string nickname;

    private List<string> easyLevels = new List<string>();

    private List<string> mediumLevels = new List<string>();

    private List<string> hardLevels = new List<string>();

    private List<RoomInfo> roomInfos = new List<RoomInfo>();

    private void Start()
    {
        FindLevels();
        PlaceLevelButton();
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
        PhotonNetwork.LocalPlayer.NickName = nickname;
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
        RoomOptions roomOptions = new RoomOptions();
        if (gameMode == "Green")
        {
            customProperties.Add("GameMode", "Green");
            roomOptions.MaxPlayers = 9;
        }
        else if (gameMode == "Green Circle")
        {
            customProperties.Add("GameMode", "Green Circle");
            roomOptions.MaxPlayers = 8;
        }
        roomOptions.CustomRoomProperties = customProperties;
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "GameMode" };
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        PhotonNetwork.CreateRoom(nickname + " " + GenerateRandomString(20), roomOptions);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("La room a été créée !");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connecté à la room -> " + PhotonNetwork.CurrentRoom.Name);
        PlacePlayerButton();
        actualPanel.SetActive(false);
        actualPanel = roomPanel;
        roomPanel.SetActive(true);
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }
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

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " a quitté la room !");
        PlacePlayerButton();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " a rejoint la room !");
        PlacePlayerButton();
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

    public void StartLevel()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties["GameMode"].ToString() == "Green")
            {
                PhotonNetwork.LoadLevel("Assets/Scenes/Levels/Green TD.unity");
            }
            else if (PhotonNetwork.CurrentRoom.CustomProperties["GameMode"].ToString() == "Green Circle")
            {
                PhotonNetwork.LoadLevel("Assets/Scenes/Levels/Green Circle TD.unity");
            }
        }
    }

    private void PlacePlayerButton()
    {
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < roomContent.childCount; i++)
        {
            Destroy(roomContent.GetChild(i).gameObject);
        }
        foreach (var player in players)
        {
            GameObject button = Instantiate(playerButton, roomContent);
            button.GetComponentInChildren<Text>().text = player.NickName;
        }
    }

    private void PlaceRoomButton()
    {
        for (int i = 0; i < listRoomContent.childCount; i++)
        {
            Destroy(listRoomContent.GetChild(i).gameObject);
        }
        for (int i = 0; i < roomInfos.Count; i++)
        {
            if (roomInfos[i].CustomProperties["GameMode"] == null || !roomInfos[i].IsOpen || !roomInfos[i].IsVisible)
            {
                roomInfos.RemoveAt(i);
            }
        }
        AutoResizeScrollView(listRoomContent, roomInfos.Count);
        foreach (RoomInfo roomInfo in roomInfos)
        {
            GameObject button = Instantiate(roomButton, listRoomContent);
            button.GetComponentInChildren<Text>().text = roomInfo.CustomProperties["GameMode"] + " : " + roomInfo.Name.Split(' ')[0];
            button.name = roomInfo.Name;
        }
    }

    private void AutoResizeScrollView(Transform content, int elements)
    {
        RectTransform rectTransform = content.GetComponent<RectTransform>();
        GridLayoutGroup gridLayoutGroup = content.GetComponent<GridLayoutGroup>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, (elements * (gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y)) + gridLayoutGroup.spacing.y);
    }

    private void PlaceLevelButton()
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

    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        System.Random random = new System.Random();
        string randomString = "";
        for (int i = 0; i < length; i++)
        {
            int nbRandom = random.Next(0, chars.Length - 1);
            randomString += chars[nbRandom].ToString();
        }
        return randomString;
    }
}
