using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ConnectionStatus : MonoBehaviour
{
    [Header("Connection Status")]
    [SerializeField] private Text connectionStatus;

    private string connectionStatusText = "Statut de connection : ";

    private void Start()
    {
        connectionStatus.text = "";
    }

    void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
            connectionStatus.text = connectionStatusText + "connect� au serveur !";
        }
        else if (PhotonNetwork.InLobby)
        {
            connectionStatus.text = connectionStatusText + "connect� au lobby !";
        }
        else if (PhotonNetwork.IsConnectedAndReady)
        {
            connectionStatus.text = connectionStatusText + "pr�t !";
        }
        else if (PhotonNetwork.InRoom)
        {
            connectionStatus.text = connectionStatusText + "connect� � la partie !";
        }
        else
        {
            connectionStatus.text = connectionStatusText + "d�connect� !";
        }
    }
}
