using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    [Header("Player Item Stuff")]
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private GameObject leftArrowButton;
    [SerializeField] private GameObject rightArrowButton;
    [SerializeField] private Color highlightColour;
    
    private Image backgroundImage;

    [Header("Player Avatars")]
    [SerializeField] private Image playerAvatar;
    [SerializeField] private Sprite[] avatars;
    private const string PLAYER_AVATAR = "playerAvatar";
    private readonly Hashtable playerProperties = new Hashtable();
    private Player player;
    
    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        playerProperties[PLAYER_AVATAR] = 0;

    }

    private void Start()
    {
    }

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        player = _player;
        UpdatePlayerItem(player);
    }

    public void ApplyLocalChanges()
    {
        backgroundImage.color = highlightColour;
        leftArrowButton.SetActive(true);
        rightArrowButton.SetActive(true);
    }

    public void OnClickLeftArrow()
    {
        if ((int)playerProperties[PLAYER_AVATAR] == 0)
        {
            playerProperties[PLAYER_AVATAR] = avatars.Length - 1;
        }
        else
        {
            playerProperties[PLAYER_AVATAR] = (int)playerProperties[PLAYER_AVATAR] - 1;
        }
        
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightArrow()
    {
        if ((int)playerProperties[PLAYER_AVATAR] == avatars.Length - 1)
        {
            playerProperties[PLAYER_AVATAR] = 0;
        }
        else
        {
            playerProperties[PLAYER_AVATAR] = (int)playerProperties[PLAYER_AVATAR] + 1;
        }

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (Equals(player, targetPlayer))
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    private void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey(PLAYER_AVATAR))
        {
            playerAvatar.sprite = avatars[(int)player.CustomProperties[PLAYER_AVATAR]];
            playerProperties[PLAYER_AVATAR] = (int)player.CustomProperties[PLAYER_AVATAR];
        }
        else
        {
            playerProperties[PLAYER_AVATAR] = 0;
        }
    }
}
