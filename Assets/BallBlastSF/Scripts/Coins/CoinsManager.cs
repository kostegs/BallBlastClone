using System;
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

    public void OnCoinCollisionHandler(object coinObject, CoinCollisionEventArgs eventArgs)
    {
        if (eventArgs._collisionTransform.GetComponent<Cart>() != null)
        {
            Coin coin = (Coin)coinObject;
            CountOfCoins += coin.Value;
            Destroy(coin.gameObject);
            OnCoinAmountChanged?.Invoke();
            DataStorage.FillDataFromCoinsManager(this);
        }
    }

    public void OnStoneDestroyedHandler(StoneDestroyedEventArgs eventArgs)
    {
        Vector2 coinPosition = eventArgs.StonePosition;

        Coin coin = _coinSpawner.SpawnCoin(coinPosition);
        coin.OnCoinCollision += OnCoinCollisionHandler;

        int maxValue = (int)(DataStorage.LevelNumber / 4) + 3;
        int randomValue = UnityEngine.Random.Range(1, maxValue + 1);

        coin.SetValue(randomValue);  
    }

    private void OnDestroy() => DataStorage.FillDataFromCoinsManager(this);

    public void SubtractCoins(int countOfCoins)
    {
        CountOfCoins -= countOfCoins;
        CountOfCoins = CountOfCoins < 0 ? 0 : CountOfCoins;
        OnCoinAmountChanged?.Invoke();
    } 
}