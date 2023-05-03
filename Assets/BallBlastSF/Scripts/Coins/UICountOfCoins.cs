using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICountOfCoins : MonoBehaviour
{
    [SerializeField] private CoinsManager _coinsManager;
    [SerializeField] private TextMeshProUGUI _countOfCoinsText;

    private int _countOfCoins;

    private void Start() => _coinsManager.OnCoinPickedUp += OnCoinPickedUpHandler;

    private void OnCoinPickedUpHandler()
    {
        _countOfCoinsText.text = _coinsManager.CountOfCoins.ToString();
    }
}
