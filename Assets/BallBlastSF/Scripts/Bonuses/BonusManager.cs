using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private StonesManager _stonesManager;
    [SerializeField] private BonusSpawner _bonusSpawner;
    [Header("Probability")]
    [SerializeField] private int _bonusProbabilityPercent;

    private void Start()
    {
        _stonesManager.OnStoneDestroyed += OnStoneDestroyedHandler;
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

        int randomNumber = Random.Range(0, 101);
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
        Debug.Log("Stone freeze");
    }
}
