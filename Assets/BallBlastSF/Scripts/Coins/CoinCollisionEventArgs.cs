using System;
using UnityEngine;

public class CoinCollisionEventArgs: EventArgs
{
    public readonly Transform _collisionTransform;

    public CoinCollisionEventArgs(Transform collisionTransform) => _collisionTransform = collisionTransform;
}
