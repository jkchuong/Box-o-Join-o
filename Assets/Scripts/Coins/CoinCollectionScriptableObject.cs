using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Coin Collection", menuName = "ScriptableObjects/Coin Collection")]
public class CoinCollectionScriptableObject : ScriptableObject
{
    private readonly List<CoinTypeScriptableObject> collectedCoins = new List<CoinTypeScriptableObject>();
    
    public event Action Changed;

    public int Count => collectedCoins.Count;
    
    public void Add(Coin coin)
    {
        collectedCoins.Add(coin.CoinTypeScriptableObject);
        Changed?.Invoke();
    }

    public int CountOf(CoinTypeScriptableObject coinTypeScriptableObject)
    {
        return collectedCoins.Count(t => t == coinTypeScriptableObject);
    }
}
