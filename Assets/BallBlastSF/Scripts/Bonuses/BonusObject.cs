using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusObject : MonoBehaviour
{
    public event EventHandler<CoinCollisionEventArgs> OnBonusCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinCollisionEventArgs eventArgs = new CoinCollisionEventArgs(collision.transform.root);
        OnBonusCollision?.Invoke(this, eventArgs);
    }

    public virtual void ApplyBonus(BonusManager bonusManager) { }
}
