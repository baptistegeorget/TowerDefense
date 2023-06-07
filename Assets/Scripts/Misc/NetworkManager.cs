using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // fleche de retour + les panels restants

    [Header("Login")]
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private InputField loginInput;

    [Header("Select")]
    [SerializeField] private GameObject selectPanel;

    [Header("List Party")]
    [SerializeField] private GameObject listPartyPanel;

    [Header("Create Party")]
    [SerializeField] private GameObject createPartyPanel;

    [Header("Lobby")]
    [SerializeField] private GameObject lobyPanel;

    private bool firstConnection = true;

    private string nickname;

    private void Update()
    {
        if (!PhotonNetwork.IsConnected && firstConnection)
        {
            selectPanel.SetActive(false);
            listPartyPanel.SetActive(false);
            createPartyPanel.SetActive(false);
            loginPanel.SetActive(false);
            loginPanel.SetActive(true);
            firstConnection = false;
        }
    }

    public void CreatePseudo()
    {
        if (loginInput.text.Length > 3)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void CreatePartyPanel()
    {
        selectPanel.SetActive(false);
        createPartyPanel.SetActive(true);
    }

    public void ListPartyPanel()
    {
        selectPanel.SetActive(false);
        listPartyPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        firstConnection = true;
        nickname = loginInput.text;
        loginPanel.SetActive(false);
        selectPanel.SetActive(true);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connecté au lobby: " + PhotonNetwork.CurrentLobby.Name);
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connecté à la room: " + PhotonNetwork.CurrentRoom.Name);
        //PhotonNetwork.Instantiate(player.name, startPoint.position, startPoint.rotation);
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom("HUB", roomOptions, null);
    }

    //   if (!photonView.isMine)
    //{
    //       return
    //}
}
