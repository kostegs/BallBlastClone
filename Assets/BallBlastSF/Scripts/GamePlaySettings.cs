using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlaySettings : MonoBehaviour
{
    [SerializeField] private float _fireRate;
    [SerializeField] private int _damage;
    [SerializeField] private int _projectileAmount;
    [SerializeField] private float _projectileDistance;

    public float FireRate => _fireRate;
    public int Damage => _damage;
    public int ProjectileAmount => _projectileAmount;
    public float ProjectileDistance => _projectileDistance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
