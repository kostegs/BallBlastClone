using System;
using UnityEngine;
public class Coin : MonoBehaviour
{
    public event EventHandler<CoinCollisionEventArgs> OnCoinCollision;
    public int Value { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinCollisionEventArgs eventArgs = new CoinCollisionEventArgs(collision.transform.root);
        OnCoinCollision?.Invoke(this, eventArgs);
    }

    public void SetValue(int value)
    {
        if (value >= 0)
            Value = value;
    }
}
