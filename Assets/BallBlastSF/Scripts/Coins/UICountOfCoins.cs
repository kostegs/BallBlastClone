using TMPro;
using UnityEngine;

public class UICountOfCoins : MonoBehaviour
{
    [SerializeField] private CoinsManager _coinsManager;
    [SerializeField] private TextMeshProUGUI _countOfCoinsText;
    
    private void Start() => _coinsManager.OnCoinAmountChanged += OnCoinAmountChangedHandler;

    private void OnCoinAmountChangedHandler() => _countOfCoinsText.text = _coinsManager.CountOfCoins.ToString();
}
