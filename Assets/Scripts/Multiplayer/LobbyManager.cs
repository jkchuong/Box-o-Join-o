using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Lobby and Rooms")]
    [SerializeField] private InputField createRoomInput;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private RoomItem roomItemPrefab;
    [SerializeField] private Transform contentObject;
    [SerializeField] private float timeBetweenRoomUpdates = 1.5f;
    [SerializeField] private GameObject playButton;

    [Header("Players")]
    [SerializeField] private PlayerItem playerItemPrefab;
    [SerializeField] private Transform playerItemParent;

    private const string PLAY_SCENE_NAME = "Play";
    private float nextUpdateTime;
    private readonly List<PlayerItem> playerItems = new List<PlayerItem>();
    private readonly List<RoomItem> roomItemList = new List<RoomItem>();
    

    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    public void OnClickCreate()
    {
        if (createRoomInput.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(createRoomInput.text, new RoomOptions{ MaxPlayers = 3, BroadcastPropsChangeToAll = true});
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenRoomUpdates;
        }
    }

    private void UpdateRoomList(List<RoomInfo> roomList)
    {
        // Empty Room List
        foreach (RoomItem item in roomItemList)
        {
            Destroy(item.gameObject);
        }
        
        roomItemList.Clear();

        // Populate Room List with Room Info
        foreach (RoomInfo roomInfo in roomList)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(roomInfo.Name);
            roomItemList.Add(newRoom);
        }
    }

    public void JoinRoom(string roomNameText)
    {
        PhotonNetwork.JoinRoom(roomNameText);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    private void UpdatePlayerList()
    {
        // Empty Player List
        foreach (PlayerItem item in playerItems)
        {
            Destroy(item.gameObject);
        }

        playerItems.Clear();

        if (PhotonNetwork.CurrentRoom == null) return;

        // Populate Player list with players in the room
        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);

            if (Equals(player.Value, PhotonNetwork.LocalPlayer))
            {
                newPlayerItem.ApplyLocalChanges();
            }
            
            playerItems.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player player)
    {
        UpdatePlayerList();
    }

    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel(PLAY_SCENE_NAME);
    }
}
