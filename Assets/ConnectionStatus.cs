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
            connectionStatus.text = connectionStatusText + "connecté au serveur !";
        }
        else if (PhotonNetwork.InLobby)
        {
            connectionStatus.text = connectionStatusText + "connecté au lobby !";
        }
        else if (PhotonNetwork.IsConnectedAndReady)
        {
            connectionStatus.text = connectionStatusText + "prêt !";
        }
        else if (PhotonNetwork.InRoom)
        {
            connectionStatus.text = connectionStatusText + "connecté à la partie !";
        }
        else
        {
            connectionStatus.text = connectionStatusText + "déconnecté !";
        }
    }
}
