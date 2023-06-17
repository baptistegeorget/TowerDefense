using Photon.Pun;

public class RoomButton : MonoBehaviourPunCallbacks
{
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(name);
    }
}
