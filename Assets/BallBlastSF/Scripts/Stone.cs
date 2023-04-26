using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StoneMovement))]
public class Stone : MonoBehaviour
{
    public enum StoneSize : int
    {
        Small,
        Normal,
        Big,
        Huge
    }

    [SerializeField] private StoneSize _size;    
    [HideInInspector] public UnityEvent HitPointsChanged; // TODO - refactoring

    private StoneMovement _stoneMovement;

    public event EventHandler<StoneCollisionEventArgs> OnStoneCollision;
    public event EventHandler<EventArgs> OnStoneHitPointsEnd;

    private int _hitPoints;
    private int _maxHitPoints;

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
        _maxHitPoints = _hitPoints;
        HitPointsChanged?.Invoke(); // TODO - refactoring
    }
        
    public void AddVerticalVelocity(float velocity) => _stoneMovement.AddVerticalVelocity(velocity); 

    public void SetHorizontalDirection(float direction) => _stoneMovement.SetHorizontalDirection(direction); 

    public void SetSize(StoneSize size)
    {
        if (size < 0)
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
        HitPointsChanged?.Invoke();

        if (_hitPoints <= 0)
            OnStoneHitPointsEnd?.Invoke(this, new EventArgs());            
    }
    
}
