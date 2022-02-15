using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CoinCollectionScriptableObject coinCollectionScriptableObject;

    private PlayerMovement playerMovement;
    private PhotonView view;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        view = GetComponent<PhotonView>();
    }

    private void Start()
    {
        playerMovement.shouldMove = view.IsMine;
    }
}
