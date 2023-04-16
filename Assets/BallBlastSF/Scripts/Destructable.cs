using UnityEngine;
using UnityEngine.Events;

public class Destructable : MonoBehaviour
{
    public int MaxHitPoints;    

    [HideInInspector] public UnityEvent Die;
    [HideInInspector] public UnityEvent HitPointsChanged;
    [HideInInspector] public UnityEvent HasDamage;

    private bool _isDie = false;
    private int _hitPoints;

    private void Start()
    {
        _hitPoints = MaxHitPoints;
        HitPointsChanged?.Invoke();
    }

    public void ApplyDamage(int damage)
    {
        _hitPoints -= damage;
        HitPointsChanged?.Invoke();
        HasDamage?.Invoke();

        if (_hitPoints <= 0)
            Kill();
    }

    public void ApplyTreatment(int quantity)
    {
        _hitPoints += quantity;
        
        if (_hitPoints > MaxHitPoints)
            _hitPoints= MaxHitPoints;

        HitPointsChanged?.Invoke();
    }

    public void Kill()
    {
        if (_isDie)
            return;

        _isDie = true;
        _hitPoints= 0;
        Die?.Invoke();
    }

    public int GetHitPoints() => _hitPoints;    
}
