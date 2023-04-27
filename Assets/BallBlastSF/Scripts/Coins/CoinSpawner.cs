using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    public Coin SpawnCoin(Vector2 position)
    {
        Coin coin = Instantiate(_coinPrefab, position, Quaternion.identity);

        return coin;
    }
}
