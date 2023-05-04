using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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

    [Header("Damage improver - Price, step")]
    [SerializeField] private int _raiseDamagePriceStep;
    [SerializeField] private int _raiseDamageStep;    
    [Header("Damage improver - UI")]
    [SerializeField] private Button _raiseDamagebtn;
    [SerializeField] private TextMeshProUGUI _raiseDamageText;

    [Header("Amount improver - Price, step")]
    [SerializeField] private int[] _raiseAmountPrices;
    [SerializeField] private int _raiseAmountStep;    
    [Header("Amount improver - UI")]
    [SerializeField] private Button _raiseAmountbtn;
    [SerializeField] private TextMeshProUGUI _raiseAmountText;

    public int RaiseDamagePrice { get; private set; }
    public int RaiseAmountPointer { get; private set; }
    public int RaiseSpeedPointer { get; private set; }

    public event Action OnFinishImproving;

    private int _coinsAmount;
    private Image _raiseSpeedButtonImage;
    private Button _raiseSpeedButton;
    private Image _raiseDamageButtonImage;
    private Button _raiseDamageButton;
    private Image _raiseAmountButtonImage;
    private Button _raiseAmountButton;

    private void Start()
    {
        // Speed
        _raiseSpeedButton = _raiseSpeedbtn.GetComponent<Button>();
        _raiseSpeedButtonImage = _raiseSpeedbtn.GetComponent<Image>();

        // Damage
        _raiseDamageButton = _raiseDamagebtn.GetComponent<Button>();
        _raiseDamageButtonImage = _raiseDamagebtn.GetComponent<Image>();

        // Amount
        _raiseAmountButton = _raiseAmountbtn.GetComponent<Button>();
        _raiseAmountButtonImage = _raiseAmountbtn.GetComponent<Image>();
    }

    public void ShowImproverForm()
    {
        RaiseDamagePrice = DataStorage.RaiseDamagePrice;
        RaiseDamagePrice = RaiseDamagePrice == 0 ? _raiseDamagePriceStep : RaiseDamagePrice;

        RaiseAmountPointer = DataStorage.RaiseAmountPointer;
        RaiseSpeedPointer = DataStorage.RaiseSpeedPointer;

        ChangeUI();

        _uiImprovingForm.OnCloseImprovingForm += OnCloseImproverFormHandler;
        _uiImprovingForm.ShowForm();
    }

    private void ChangeUI()
    {
        _coinsAmount = _coinsManager.CountOfCoins;

        ChangeUI_Speed();
        ChangeUI_Damage();
        ChangeUI_Amount();
    }

    private void ChangeUI_Speed()
    {
        int currentPrice = _raiseSpeedPrices[RaiseSpeedPointer];
        _raiseSpeedText.text = currentPrice.ToString();

        if (_coinsAmount < currentPrice)
            BlockImproverButton(_raiseSpeedButton, _raiseSpeedButtonImage);
    }

    private void ChangeUI_Damage()
    {
        _raiseDamageText.text = RaiseDamagePrice.ToString();

        if (_coinsAmount < RaiseDamagePrice)        
            BlockImproverButton(_raiseDamageButton, _raiseDamageButtonImage);                    
    }

    private void ChangeUI_Amount()
    {
        if (RaiseAmountPointer >= _raiseAmountPrices.Length)
        {
            BlockImproverButton(_raiseAmountButton, _raiseAmountButtonImage, _raiseAmountText, "MAX");
            return;
        }

        int currentPrice = _raiseAmountPrices[RaiseAmountPointer];
        _raiseAmountText.text = currentPrice.ToString();

        if (_coinsAmount < currentPrice)        
            BlockImproverButton(_raiseAmountButton, _raiseAmountButtonImage);                    
    }

    private void BlockImproverButton(Button button, Image buttonImage)
    {
        button.enabled = false;
        buttonImage.sprite = _bwCoinSprite;
    }

    private void BlockImproverButton(Button button, Image buttonImage, TextMeshProUGUI buttonTextComponent, string buttonText)
    {
        BlockImproverButton(button, buttonImage);
        buttonTextComponent.text = buttonText;
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

    public void RaiseDamage()
    {
        _gamePlaySettings.Damage += _raiseDamageStep;
        _coinsManager.SubtractCoins(RaiseDamagePrice);
        RaiseDamagePrice += _raiseDamagePriceStep;
        ChangeUI();
    }

    public void RaiseAmount()
    {
        _gamePlaySettings.ProjectileAmount += _raiseAmountStep;
        _coinsManager.SubtractCoins(_raiseAmountPrices[RaiseAmountPointer]);
        RaiseAmountPointer++;
        ChangeUI();
    }

    private void OnDestroy()
    {
        DataStorage.FillDataFromCharacteristicsImprover(this);
    }

}
