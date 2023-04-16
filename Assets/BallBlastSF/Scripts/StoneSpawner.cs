using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class StoneSpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private Stone _stonePrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnRate;

    [Header("Balance")]
    [SerializeField] private Turret _turret;
    [SerializeField] private int _amount;
    [SerializeField][Range(0.0f, 1.0f)] private float _minHitPointsPercentage;    
    [SerializeField] private float _maxHitPointsRate;

    [Space(10)] public UnityEvent Completed;

    private float _timer;
    private int _amountSpawned;
    private int _stoneMaxHitPoints;
    private int _stoneMinHitPoints;

    private void Start()
    {
        int damagePerSecond = (int)((_turret.Damage * _turret.ProjectileAmount) * (1 / _turret.FireRate));

        _stoneMaxHitPoints = (int)(damagePerSecond * _maxHitPointsRate);
        _stoneMinHitPoints = (int)(_stoneMaxHitPoints * _minHitPointsPercentage);

        _timer = _spawnRate;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnRate)
        {
            Spawn();
            _timer = 0;
        }

        if (_amountSpawned == _amount)
        {
            enabled = false;
            Completed.Invoke();
        }
            
    }

    private void Spawn()
    {
        Stone stone = Instantiate(_stonePrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
        stone.SetSize((Stone.Size)Random.Range(1, 4));
        stone.MaxHitPoints = Random.Range(_stoneMinHitPoints, _stoneMaxHitPoints + 1);
        Debug.Log($"stone.MaxHitPoints {stone.MaxHitPoints}");

        // Set Max HitPoints in dependence of stone size
        int decreasePercent = 60 - ((int)(stone.StoneSize) * 20);
        Debug.Log($"decreasePercent {decreasePercent}");

        stone.MaxHitPoints = stone.MaxHitPoints - (stone.MaxHitPoints * decreasePercent) / 100;
        Debug.Log($"stone.MaxHitPoints {stone.MaxHitPoints}");

        _amountSpawned++;
    }


}
