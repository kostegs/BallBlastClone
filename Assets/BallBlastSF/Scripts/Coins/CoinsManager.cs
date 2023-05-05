using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private CoinSpawner _coinSpawner;
    [SerializeField] private StonesManager _stonesManager;

    public event Action OnCoinAmountChanged;

    public int CountOfCoins { get; private set; }

    private void Start()
    {
        _stonesManager.OnStoneDestroyed += OnStoneDestroyedHandler;
        CountOfCoins = DataStorage.CountOfCoins;
        OnCoinAmountChanged?.Invoke();
    }

    public void OnCoinCollisionHandler(object coin, CoinCollisionEventArgs eventArgs)
    {
        if (eventArgs._collisionTransform.GetComponent<Cart>() != null)
        {
            CountOfCoins++;
            Destroy(((Coin)coin).gameObject);
            OnCoinAmountChanged?.Invoke();
        }
    }

    public void OnStoneDestroyedHandler(StoneDestroyedEventArgs eventArgs)
    {
        Vector2 coinPosition = eventArgs.StonePosition;

        Coin coin = _coinSpawner.SpawnCoin(coinPosition);
        coin.OnCoinCollision += OnCoinCollisionHandler;
    }

    private void OnDestroy()
    {
        DataStorage.FillDataFromCoinsManager(this);
    }

    public void SubtractCoins(int countOfCoins)
    {
        CountOfCoins -= countOfCoins;
        CountOfCoins = CountOfCoins < 0 ? 0 : CountOfCoins;
        OnCoinAmountChanged?.Invoke();
    } 

}
