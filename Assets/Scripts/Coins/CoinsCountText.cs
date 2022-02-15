using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsCountText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private CoinCollectionScriptableObject coinCollectionScriptableObject;

    private void Awake() => coinCollectionScriptableObject.Changed += UpdateText;

    private void OnDisable() => coinCollectionScriptableObject.Changed -= UpdateText;

    private void OnEnable() => UpdateText();

    private void OnValidate() => text = GetComponent<TMP_Text>();

    private void UpdateText() => text.SetText("Yellow Coins: " + coinCollectionScriptableObject.Count);
}
