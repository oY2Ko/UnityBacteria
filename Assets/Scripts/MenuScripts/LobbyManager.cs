using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField PlayerNickname;
    [SerializeField] InputField RoomNameToCreate;
    [SerializeField] InputField RoomNameToEnter;
    [SerializeField] Button CreateRoom;
    [SerializeField] Button JoinRoom;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NickName = PlayerNickname.text;

        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.GameVersion = "1";

        PhotonNetwork.ConnectUsingSettings();

    }


    public override void OnConnectedToMaster()
    {
        print("Connected to master");
    }

    public void OnCreateRoomButtonClicked()
    {
        PhotonNetwork.CreateRoom(RoomNameToCreate.text, new Photon.Realtime.RoomOptions { MaxPlayers = 2});

    }

    public void OnJoinRoomButtonClicked()
    {
        PhotonNetwork.JoinRoom(RoomNameToEnter.text);

    }

    public override void OnJoinedRoom()
    {
        print("Entered to room");
        PhotonNetwork.LoadLevel(1);
    }

}
