using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private CoinSpawner _coinSpawner;
    [SerializeField] private StonesManager _stonesManager;

    private int _countOfCoins;

    public event Action OnCoinPickedUp;

    public int CountOfCoins { get { return _countOfCoins; } 
                              set { if (value < 0) _countOfCoins = 0; else _countOfCoins = value; } }

    private void Start()
    {
        _stonesManager.OnStoneDestroyed += OnStoneDestroyedHandler;
        CountOfCoins = DataStorage.CountOfCoins;
        OnCoinPickedUp?.Invoke();
    }

    public void OnCoinCollisionHandler(object coin, CoinCollisionEventArgs eventArgs)
    {
        if (eventArgs._collisionTransform.GetComponent<Cart>() != null)
        {
            CountOfCoins++;
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

    private void OnDestroy()
    {
        DataStorage.FillDataFromCoinsManager(this);
    }

}
