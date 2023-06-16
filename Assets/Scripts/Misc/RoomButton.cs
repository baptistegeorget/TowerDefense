using Photon.Pun;

public class RoomButton : MonoBehaviourPunCallbacks
{
    public string roomName;

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
