using System;
using UnityEngine;
using UnityEngine.Events;

public class StoneCollisionEventArgs : EventArgs
{
    public readonly Transform _collisionTransform;

    public StoneCollisionEventArgs(Transform collisionTransform)
    {
        _collisionTransform = collisionTransform;
    }
}

[RequireComponent(typeof(StoneMovement))]
public class Stone : Destructable
{
    public enum Size : int
    {
        Small,
        Normal,
        Big,
        Huge
    }

    [SerializeField] private Size _size;
    [SerializeField] private float _spawnUpForce;

    private StoneMovement _stoneMovement;
    public event EventHandler<StoneCollisionEventArgs> OnStoneCollision;

    public static int StoneCounter { get; private set; }
    public Size StoneSize => _size;

    private void Awake()
    {
        _stoneMovement = GetComponent<StoneMovement>();
        SetSize(_size);

        Die.AddListener(OnStoneDestroyed);
        StoneCounter++;
        transform.position = new Vector3(transform.position.x, transform.position.y, -(float)0.001 * StoneCounter);

        // Reset counter of stones (we use it for changing order to Z-cooordinate for new stones)
        if (StoneCounter == 100)
            StoneCounter= 0;
    }

    private void OnDestroy()
    {
        Die.RemoveListener(OnStoneDestroyed);     
    }

    private void OnStoneDestroyed()
    {
        if (_size != Size.Small)
            SpawnStones();

        Destroy(gameObject);        
    }

    private void SpawnStones()
    {
        for (int i = 0; i < 2; i++)
        {
            Stone stone = Instantiate(this, transform.position, Quaternion.identity);
            stone.SetSize(_size - 1);
            stone.MaxHitPoints = Mathf.Clamp(MaxHitPoints / 2, 1, MaxHitPoints);
            stone._stoneMovement.AddVerticalVelocity(_spawnUpForce);
            stone._stoneMovement.SetHorizontalDirection((i % 2 * 2) - 1);
        }
    }

    public void SetSize(Size size)
    {
        if (size < 0)
            return;

        transform.localScale = GetVectorFromSize(size);
        _size = size;
    }

    private Vector3 GetVectorFromSize(Size size)
    {
        switch (size)
        {
            case Size.Huge:
                return new Vector3(1, 1, 1);
            case Size.Big:
                return new Vector3(0.75f, 0.75f, 0.75f);
            case Size.Normal:
                return new Vector3(0.6f, 0.6f, 0.6f);
            case Size.Small:
                return new Vector3(0.4f, 0.4f, 0.4f);
            default:
                return Vector3.one;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StoneCollisionEventArgs eventArgs = new StoneCollisionEventArgs(collision.transform.root);
        OnStoneCollision?.Invoke(this, eventArgs);
    }

}
