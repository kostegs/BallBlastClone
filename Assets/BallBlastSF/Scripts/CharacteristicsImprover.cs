using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicsImprover : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private CoinsManager _coinsManager;
    [SerializeField] private GamePlaySettings _gamePlaySettings;

    [Header("UI - common elements")]
    [SerializeField] private Sprite _coinSprite;
    [SerializeField] private Sprite _bwCoinSprite;

    [Header("Main form")]
    [SerializeField] private UIImprovingForm _uiImprovingForm;

    [Header("Speed improver - Price, step")]
    [SerializeField] private int[] _raiseSpeedPrices;
    [SerializeField] private float _raiseSpeedStep;
    [Header("Speed improver - UI")]
    [SerializeField] private Button _raiseSpeedbtn;
    [SerializeField] private TextMeshProUGUI _raiseSpeedText;

    public int RaiseDamagePrice { get; private set; }
    public int RaiseAmountPrice { get; private set; }
    public int RaiseSpeedPointer { get; private set; }

    public event Action OnFinishImproving;

    private int _coinsAmount;
    private Image _raiseSpeedButtonImage;
    private Button _raiseSpeedButton;
    
    private void Start()
    {
        _raiseSpeedButton = _raiseSpeedbtn.GetComponent<Button>();
        _raiseSpeedButtonImage = _raiseSpeedbtn.GetComponent<Image>();                
    }

    public void ShowImproverForm()
    {
        RaiseDamagePrice = DataStorage.RaiseDamagePrice;
        RaiseAmountPrice = DataStorage.AmountPrice;
        RaiseSpeedPointer = DataStorage.RaiseSpeedPointer;

        ChangeUI();

        _uiImprovingForm.OnCloseImprovingForm += OnCloseImproverFormHandler;
        _uiImprovingForm.ShowForm();
    }

    private void ChangeUI()
    {
        _coinsAmount = _coinsManager.CountOfCoins;

        ChangeUI_Speed();
    }

    private void ChangeUI_Speed()
    {
        int currentPrice = _raiseSpeedPrices[RaiseSpeedPointer];
        _raiseSpeedText.text = currentPrice.ToString();

        if (_coinsAmount < currentPrice)
        {
            _raiseSpeedButtonImage.sprite = _bwCoinSprite;
            _raiseSpeedButton.enabled = false;        
        }
    }

    public void OnCloseImproverFormHandler()
    {
        OnFinishImproving?.Invoke();
    }

    public void RaiseSpeed()
    {
        _gamePlaySettings.FireRate -= _raiseSpeedStep;
        _coinsManager.SubtractCoins(_raiseSpeedPrices[RaiseSpeedPointer]);
        RaiseSpeedPointer++;
        ChangeUI();
    }

    private void OnDestroy()
    {
        DataStorage.FillDataFromCharacteristicsImprover(this);
    }

}
