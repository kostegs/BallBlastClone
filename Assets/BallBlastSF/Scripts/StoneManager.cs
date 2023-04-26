using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] private StoneSpawner _spawner;

    [Header("Tempa")]
    [SerializeField] private Turret _turret; // ToDo - get values from Characteristics Storage

    [Header("Balance")]
    [SerializeField] private float _spawnRate; // same as above
    [SerializeField][Range(0.0f, 1.0f)] private float _minHitPointsPercentage;
    [SerializeField] private float _maxHitPointsRate;
    [SerializeField] private int _stonesAmount;


    private int _stonesMaxHitPoints;
    private int _stonesMinHitPoints;
    private float _timer;
    private int _amountSpawned;
    private int _currentStonesAmount;
    private int[] _stoneSizes;


    private void Start()
    {
        int damagePerSecond = (int)((_turret.Damage * _turret.ProjectileAmount) * (1 / _turret.FireRate));

        _stonesMaxHitPoints = (int)(damagePerSecond * _maxHitPointsRate);
        _stonesMinHitPoints = (int)(_stonesMaxHitPoints * _minHitPointsPercentage);

        _timer = _spawnRate;

        CreateStonesCharacteristics();        
    }

    private void CreateStonesCharacteristics()
    {
        _stoneSizes = new int[_stonesAmount];

        for (int i = 0; i < _stonesAmount; i++)
        {
            int stoneSize = Random.Range(1, 4);
            _stoneSizes[i] = stoneSize;
        }
    }

    private void Update()
    {
        if (_amountSpawned != _stonesAmount)
        {
            _timer += Time.deltaTime;

            if (_timer >= _spawnRate)
            {
                SpawnStone();
                _timer = 0;
            }
        }        
    }

    private void SpawnStone()
    {
        int currentStoneSize = _stoneSizes[_amountSpawned];

        int maxHitPoints = Random.Range(_stonesMinHitPoints, _stonesMaxHitPoints + 1);
        
        int decreasePercent = 60 - (currentStoneSize * 20);
        
        maxHitPoints = maxHitPoints - (maxHitPoints * decreasePercent) / 100;

        _spawner.SpawnStone(currentStoneSize, maxHitPoints);

        _amountSpawned++;
    }
}
