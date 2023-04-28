using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private CoinSpawner _coinSpawner;
    [SerializeField] private StonesManager _stonesManager;

    public Action OnCoinPickedUp;

    private void Start()
    {
        _stonesManager.OnStoneDestroyed += OnStoneDestroyedHandler;
    }

    public void OnCoinCollisionHandler(object coin, CoinCollisionEventArgs eventArgs)
    {
        if (eventArgs._collisionTransform.GetComponent<Cart>() != null)
        {
            Debug.Log("Turret vs Coin Collision!");

            Destroy(((Coin)coin).gameObject);
            OnCoinPickedUp?.Invoke();
        }
    }

    public void OnStoneDestroyedHandler(StoneDestroyedEventArgs _eventArgs)
    {
        Vector2 coinPosition = _eventArgs.StonePosition;

        Coin coin = _coinSpawner.SpawnCoin(coinPosition);
        coin.OnCoinCollision += OnCoinCollisionHandler;
    }

}
