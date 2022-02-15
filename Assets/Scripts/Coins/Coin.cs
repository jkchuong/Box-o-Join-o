using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotationLength = 2;
    [SerializeField] private CoinTypeScriptableObject coinTypeScriptableObject;

    public CoinTypeScriptableObject CoinTypeScriptableObject => coinTypeScriptableObject;

    private void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), rotationLength, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().color = coinTypeScriptableObject.coinColour;
    }

    private void OnValidate()
    {
        gameObject.name = coinTypeScriptableObject.name;
        GetComponent<SpriteRenderer>().color = coinTypeScriptableObject.coinColour;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.coinCollectionScriptableObject.Add(this);
            gameObject.SetActive(false);
        }
    }
}
