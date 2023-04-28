using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StoneMovement))]
public class Stone : MonoBehaviour
{
    public enum StoneSize : int
    {
        Small = 1,
        Normal = 2,
        Big = 3,
        Huge = 4
    }

    [SerializeField] private StoneSize _size;    
    
    private StoneMovement _stoneMovement;

    public event EventHandler<StoneCollisionEventArgs> OnStoneCollision;
    public event EventHandler<EventArgs> OnStoneHitPointsEnd;
    public event EventHandler<EventArgs> OnHitPointsChanged;

    private int _hitPoints;
    private int _maxHitPoints;
    private EventArgs _eventArgs;

    public static int StoneCounter { get; private set; }    
    public StoneSize Size => _size;
    public int HitPoints { get => _hitPoints; set { _hitPoints = (value == 0 ? 0 : value); } }    
    public int MaxHitPoints => _maxHitPoints;

    private void Awake()
    {
        _stoneMovement = GetComponent<StoneMovement>();
        SetSize(_size);
        
        StoneCounter++;
        transform.position = new Vector3(transform.position.x, transform.position.y, -(float)0.001 * StoneCounter);                
    }

    private void Start()
    {
        _eventArgs = new EventArgs();
        _maxHitPoints = _hitPoints;
        OnHitPointsChanged?.Invoke(this, _eventArgs);        
    }
        
    public void AddVerticalVelocity(float velocity) => _stoneMovement.AddVerticalVelocity(velocity); 

    public void SetHorizontalDirection(float direction) => _stoneMovement.SetHorizontalDirection(direction); 

    public void SetSize(StoneSize size)
    {
        if (size <= 0)
            return;

        transform.localScale = GetVectorFromSize(size);
        _size = size;
    }

    private Vector3 GetVectorFromSize(StoneSize size)
    {
        switch (size)
        {
            case StoneSize.Huge:
                return new Vector3(1, 1, 1);                
            case StoneSize.Big:
                return new Vector3(0.75f, 0.75f, 0.75f);                
            case StoneSize.Normal:
                return new Vector3(0.6f, 0.6f, 0.6f);
            case StoneSize.Small:
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

    public void ApplyDamage(int damage)
    {
        _hitPoints -= damage;
        OnHitPointsChanged?.Invoke(this, _eventArgs);

        if (_hitPoints <= 0)
            OnStoneHitPointsEnd?.Invoke(this, _eventArgs);            
    }
    
}
