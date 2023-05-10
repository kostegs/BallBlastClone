using System;
using UnityEngine;

public class StoneCollisionEventArgs : EventArgs
{
    public readonly Transform _collisionTransform;

    public StoneCollisionEventArgs(Transform collisionTransform) => _collisionTransform = collisionTransform;
}
