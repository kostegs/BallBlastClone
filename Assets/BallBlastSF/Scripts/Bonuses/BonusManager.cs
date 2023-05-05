using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private StonesManager _stonesManager;
    [SerializeField] private BonusSpawner _bonusSpawner;
    [Header("Probability")]
    [SerializeField] private int _bonusProbabilityPercent;
    [Header("Stones Freezer")]
    [SerializeField] private int _timeForFreeze;
    [Header("UI")]
    [SerializeField] private GameObject _uiFreezingPanel;
    [SerializeField] private TextMeshProUGUI _freezeTimerText;

    private float _freezingTimer;
    private bool _freezeState;

    private void Start()
    {
        _stonesManager.OnStoneDestroyed += OnStoneDestroyedHandler;
    }

    private void Update()
    {
        if (_freezeState)
        {
            _freezingTimer -= Time.deltaTime;
            _freezeTimerText.text = Mathf.Round(_freezingTimer).ToString();

            if (_freezingTimer <= 0)
            {
                _freezeState = false;
                ApplyUnfreezeStones();
            }
        }
    }

    public void OnStoneDestroyedHandler(StoneDestroyedEventArgs eventArgs)
    {
        if (BonusIsPossible() == false)
            return;

        BonusObject bonusObject = _bonusSpawner.SpawnRandomBonus(eventArgs.StonePosition);
        bonusObject.OnBonusCollision += OnBonusCollisionHandler;        
    }

    public bool BonusIsPossible()
    {
        int lowerThreshold = 100 - _bonusProbabilityPercent;

        int randomNumber = Random.Range(1, 101);
        //Debug.Log(randomNumber);

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
        Debug.Log("Cart indestructible");
    }

    public void ApplyStoneFreeze()
    {
        _freezeState = true;
        _freezingTimer = (float)_timeForFreeze;
        _stonesManager.FreezeStones();
        ChangeUI();
    }

    private void ApplyUnfreezeStones()
    {
        _freezeState = false;
        _stonesManager.UnFreezeStones();
        ChangeUI();
    }

    private void ChangeUI()
    {
        _uiFreezingPanel.SetActive(_freezeState);
    }
}
