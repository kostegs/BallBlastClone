using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event EventHandler<CoinCollisionEventArgs> OnCoinCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinCollisionEventArgs eventArgs = new CoinCollisionEventArgs(collision.transform.root);
        OnCoinCollision?.Invoke(this, eventArgs);
    }
}
