using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private StonesManager _stonesManager;
    [SerializeField] private BonusSpawner _bonusSpawner;
    [SerializeField] private Cart _cart;
    [Header("Probability")]
    [SerializeField] private int _bonusProbabilityPercent;    
    [Header("Stones Freezer")]
    [SerializeField] private int _timeForFreeze;
    [Header("Cart unbreakable")]
    [SerializeField] private int _timeForUnbreakable;
    [Header("UI")]
    [SerializeField] private GameObject _uiBonusPanel;
    [SerializeField] private TextMeshProUGUI _bonusText;
    [SerializeField] private TextMeshProUGUI _bonusTimerText;

    private float _freezingTimer;
    private bool _freezeState;
    private float _unbreakableTimer;
    private bool _unbreakableState;
    private bool _bonusSpawnIsPossible;

    private void Start()
    {
        _stonesManager.OnStoneDestroyed += OnStoneDestroyedHandler;
        _bonusSpawnIsPossible = true;
    }

    private void Update()
    {
        UpdateFreezeState();
        UpdateUnbreakableState();
    }

    private void UpdateFreezeState()
    {
        if (_freezeState)
        {
            _freezingTimer -= Time.deltaTime;
            _bonusTimerText.text = Mathf.Round(_freezingTimer).ToString();

            if (_freezingTimer <= 0)
            {
                _freezeState = false;
                ApplyUnfreezeStones();
            }
        }
    }

    private void UpdateUnbreakableState()
    {
        if (_unbreakableState)
        {
            _unbreakableTimer -= Time.deltaTime;
            _bonusTimerText.text = Mathf.Round(_unbreakableTimer).ToString();

            if (_unbreakableTimer <= 0)
            {
                _unbreakableState = false;
                ApplyCartBreakable();
            }
        }
    }

    public void OnStoneDestroyedHandler(StoneDestroyedEventArgs eventArgs)
    {
        if (BonusIsPossible() == false)        
            return;
        
        BonusObject bonusObject = _bonusSpawner.SpawnRandomBonus(eventArgs.StonePosition);
        bonusObject.OnBonusCollision += OnBonusCollisionHandler;
        _bonusSpawnIsPossible = false;
    }

    public bool BonusIsPossible()
    {
        if (_bonusSpawnIsPossible == false)
            return false;

        int lowerThreshold = 100 - _bonusProbabilityPercent;

        int randomNumber = Random.Range(1, 101);
        
        return randomNumber > lowerThreshold;
    }

    private void OnBonusCollisionHandler(object sender, CoinCollisionEventArgs eventArgs)
    {
        if (eventArgs._collisionTransform.GetComponent<Cart>() != null)
        {
            BonusObject bonusObject = (BonusObject)sender;
            bonusObject.ApplyBonus(this);   
            Destroy(bonusObject.gameObject);            
        }
    }

    public void ApplyCartIndestructible()
    {
        _unbreakableState = true;
        _unbreakableTimer = _timeForUnbreakable; 
        _cart.SetUnbreakableState(true);
        ChangeUI();
    }

    public void ApplyCartBreakable()
    {
        _unbreakableState = false;
        _cart.SetUnbreakableState(false);
        ChangeUI();
        ChangeBonusSpawnAbility();
    }

    public void ApplyStoneFreeze()
    {
        _freezeState = true;
        _freezingTimer = _timeForFreeze;
        _stonesManager.FreezeStones();
        ChangeUI();
    }

    private void ApplyUnfreezeStones()
    {
        _freezeState = false;
        _stonesManager.UnFreezeStones();
        ChangeUI();
        ChangeBonusSpawnAbility();
    }

    private void ChangeUI()
    {
        if (_unbreakableState)
            _bonusText.text = "Immortal!";
        else if (_freezeState)
            _bonusText.text = "Freezed!";

        _uiBonusPanel.SetActive(_freezeState || _unbreakableState);
    }

    private void ChangeBonusSpawnAbility()
    {
        _bonusSpawnIsPossible = true;
    }
}
