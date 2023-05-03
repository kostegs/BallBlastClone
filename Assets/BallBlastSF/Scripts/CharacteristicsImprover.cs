using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicsImprover : MonoBehaviour
{
    [SerializeField] private int[] _raiseSpeedPrices;
    [SerializeField] private CoinsManager _coinsManager;
    [SerializeField] private GamePlaySettings _gamePlaySettings;

    [Header("UI")]
    [SerializeField] private Sprite _coinSprite;
    [SerializeField] private Sprite _bwCoinSprite;

    [SerializeField] private UIImprovingForm _uiImprovingForm;
    [SerializeField] private Button _raiseSpeedbtn;

    private int _raiseSpeedPointer;

    private void Start()
    {
        ChangeUI();    
    }

    private void ChangeUI()
    {
        _raiseSpeedbtn.GetComponent<Image>().sprite = _bwCoinSprite;
    }

    public void RaiseSpeed()
    {
        _gamePlaySettings.FireRate -= 0.01f;
    }

}
