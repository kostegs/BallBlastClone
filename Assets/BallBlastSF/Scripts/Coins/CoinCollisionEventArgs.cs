using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollisionEventArgs: EventArgs
{
    public readonly Transform _collisionTransform;

    public CoinCollisionEventArgs(Transform collisionTransform)
    {
        _collisionTransform = collisionTransform;
    }
}
