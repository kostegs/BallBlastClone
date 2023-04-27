using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private CoinSpawner _coinSpawner;

    private void Start()
    {
        Coin coin = _coinSpawner.SpawnCoin(new Vector2(-6, 3));
        coin.OnCoinCollision += OnCoinCollisionHandler;

    }

    public void OnCoinCollisionHandler(object coin, CoinCollisionEventArgs eventArgs)
    {
        if (eventArgs._collisionTransform.GetComponent<Cart>() != null)
        {
            Debug.Log("Turret vs Coin Collision!");

            Destroy(((Coin)coin).gameObject);
        }
    }

}
