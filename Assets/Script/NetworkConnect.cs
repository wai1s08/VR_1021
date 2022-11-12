using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkConnect : MonoBehaviourPunCallbacks
{
    public static NetworkConnect Instance;

    public GameObject playerCamera;

    public Transform headPos;
    public Transform rightHandPos;
    public Transform leftHandPos;

    public Transform startPos;
    void Start()
    {
        Instance = this;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.LocalPlayer.NickName = "Monster";
    }

    private void Update()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            Debug.LogError(PhotonNetwork.CurrentRoom.PlayerCount);
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.LogError("連線成功");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.LogError("加入大廳成功");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom("1028", roomOptions, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        Debug.LogError("加入房間成功");
        playerCamera.transform.position = startPos.position;
        PhotonNetwork.Instantiate("Player", startPos.position, startPos.rotation);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogError($"{newPlayer.NickName}加入房間");
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogError($"{otherPlayer.NickName}離開房間");
    }
}